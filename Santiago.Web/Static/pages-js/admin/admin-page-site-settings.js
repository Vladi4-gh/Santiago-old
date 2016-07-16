(function($, _) {
  "use strict";

  var $editSiteSettingsForm = $(".js-edit-site-settings-form");
  var $editSiteSettingsFormSaveButton = $editSiteSettingsForm.find(".js-save-button");
  var $editSiteSettingsFormSpinnerWrapper = $editSiteSettingsForm.find(".js-spinner-wrapper");
  var $editSiteSettingsFormSuccessMessage = $editSiteSettingsForm.find(".js-success-message");

  var $mainPageIdHiddenInput = $editSiteSettingsForm.find(".js-selected-main-page-editor input:hidden");
  var selectedMainPageInfoTemplateFunction = doT.template("{{? it && it.id >= 0 }}Выбранная главная страница: {{= it.title }} (ID: {{= it.id }}){{??}}<span class='empty-value-text'>Главная страница не выбрана</span>{{?}}");
  var $selectMainPagePopupPagesDataTable;
  var $selectMainPagePopupSelectedMainPageInfo;
  var $selectMainPagePopupSelectedMainPageRemoveButton;

  function createSelectMainPagePopupPagesDataTable($element) {
    $element.dataTable({
      columns: [
        {
          field: "title",
          title: "Заголовок"
        },
        {
          field: "alias",
          title: "Псевдоним"
        },
        {
          field: "creationDate",
          title: "Дата создания",
          isHtml: true,
          templateName: "date",
          width: 150
        }
      ],
      templates: [
        {
          name: "date",
          template: "{{= DateFormat.format.date(it, 'dd.MM.yyyy H:mm:ss') }}"
        }
      ],
      actions: {
        getAction: {
          url: "/Admin/Pages/GetPagesOrderedByCreationDateDesc"
        }
      },
      editable: false,
      minWidth: 700,
      height: 369,
      highlightLastClickedRow: true,
      addClickableRowsCssClass: true,
      onRowClicked: onSelectMainPagePopupPagesDataTableRowClicked
    });
  }

  function updateSiteSettings($editSiteSettingsForm) {
    $.ajax({
      type: "POST",
      url: "/Admin/Site-Settings/UpdateSiteSettings",
      data: $editSiteSettingsForm.serialize(),
      beforeSend: function() {
        disableEditSiteSettingsFormSaveButton();
        hideSuccessMessage();
        showSpinner();
      },
      success: function(serverValidationErrorsObject) {
        if (serverValidationErrorsObject) {
          $editSiteSettingsForm.validate().showErrors(JSON.parse(serverValidationErrorsObject));
          enableEditSiteSettingsFormSaveButton();
          hideSpinner();
        } else {
          enableEditSiteSettingsFormSaveButton();
          hideSpinner();
          showSuccessMessage();
          hideSuccessMessage();
        }
      },
      error: function() {
        alert("Произошла неизвестная ошибка!");

        enableEditSiteSettingsFormSaveButton();
        hideSpinner();
      }
    });
  }

  function enableEditSiteSettingsFormSaveButton() {
    $editSiteSettingsFormSaveButton
      .removeClass("disabled")
      .prop("disabled", false);
  }

  function disableEditSiteSettingsFormSaveButton() {
    $editSiteSettingsFormSaveButton
      .addClass("disabled")
      .prop("disabled", true);
  }

  function showSpinner() {
    $editSiteSettingsFormSpinnerWrapper.removeClass("hidden");
  }

  function hideSpinner() {
    $editSiteSettingsFormSpinnerWrapper.addClass("hidden");
  }

  function showSuccessMessage() {
    $editSiteSettingsFormSuccessMessage.removeClass("hidden");
  }

  function hideSuccessMessage() {
    $editSiteSettingsFormSuccessMessage
      .stop(true, true)
      .delay(3000)
      .fadeOut(3000, function() {
        $(this)
          .addClass("hidden")
          .css("display", "");
      });
  }

  function onSelectMainPagePopupPagesDataTableRowClicked(selectedItemId, $clickedRow) {
    var selectedItem = _.find($selectMainPagePopupPagesDataTable.data("dataTable").dataSource, function(x) {
      return x.id === selectedItemId;
    });

    $selectMainPagePopupSelectedMainPageInfo.html(selectedMainPageInfoTemplateFunction(selectedItem));
    $selectMainPagePopupSelectedMainPageRemoveButton.css("display", "inline-block");
  }

  function resetMainPageSelection() {
    $selectMainPagePopupPagesDataTable.data("dataTable").removeRowSelection();
    $selectMainPagePopupSelectedMainPageInfo.html(selectedMainPageInfoTemplateFunction(null));
    $selectMainPagePopupSelectedMainPageRemoveButton.css("display", "none");
  }

  function initializeEventHandlers() {
    $editSiteSettingsForm.on("submit", function(e) {
      e.preventDefault();

      if ($editSiteSettingsForm.valid()) {
        updateSiteSettings($editSiteSettingsForm);
      }
    });

    $(".js-main-settings-section .js-selected-main-page-info").on("click", function() {
      var $selectedMainPageInfo = $(this);
      var $selectedMainPageId = parseInt($mainPageIdHiddenInput.val(), 10);
      var $selectMainPagePopup = $("<div></div>").appendTo(".edit-site-settings-page").popup({
        title: "Выбор главной страницы",
        content: $(".popups .js-select-main-page-popup").html()
      });

      $selectMainPagePopupPagesDataTable = $selectMainPagePopup.find(".js-pages-data-table");
      createSelectMainPagePopupPagesDataTable($selectMainPagePopupPagesDataTable);

      $selectMainPagePopupSelectedMainPageInfo = $selectMainPagePopup.find(".js-selected-main-page-info");
      $selectMainPagePopupSelectedMainPageRemoveButton = $selectMainPagePopup.find(".js-selected-main-page-remove-button");

      if ($selectedMainPageId !== NaN && $selectedMainPageId > 0) {
        $selectMainPagePopupPagesDataTable.data("dataTable").selectRow($selectedMainPageId);

        /*$selectMainPagePopupSelectedMainPageInfo.html(selectedMainPageInfoTemplateFunction({
          selectedMainPageInfo: $selectedMainPageInfo.text()
        }));*/

        $selectMainPagePopupSelectedMainPageRemoveButton.css("display", "inline-block");
      } else {
        /*$selectMainPagePopupSelectedMainPageInfo
          .addClass("empty-value-text")
          .html(selectedMainPageInfoEmptyValueText);*/
      }

      $selectMainPagePopupSelectedMainPageRemoveButton.on("click", function() {
        resetMainPageSelection();
      });

      $selectMainPagePopup.on("click", ".js-command-buttons .save-button", function() {
        $mainPageIdHiddenInput.val($selectMainPagePopupPagesDataTable.data("dataTable").settings.currentSelectedItemId);

        $selectMainPagePopup.data("popup").destroyPopup();
      });

      $selectMainPagePopup.on("click", ".js-command-buttons .close-button", function() {
        $selectMainPagePopup.data("popup").destroyPopup();
      });
    });
  }

  $(function() {
    initializeEventHandlers();
  });
})(jQuery, _);