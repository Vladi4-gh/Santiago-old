(function($, _) {
  "use strict";

  var $pagesDataTable = $(".js-pages-data-table");
  var $parentPagesDataTable;
  var $selectedParentPageInfo;
  var $selectedParentPageRemoveButton;
  var selectedParentPageInfoEmptyValueText = "Родительская страница не выбрана";
  var selectedParentPageInfoTemplateFunction = doT.template("Выбранная родительская страница: {{= it.parentPageTitle }} (ID: {{= it.parentPageId }})");
  var selectedParentPageId;
  var selectedParentPageTitle;
  var selectedPageParentPageId;

  function createPagesDataTable() {
    $pagesDataTable.dataTable({
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
        },
        {
          commands: ["edit", "delete"],
          width: 70
        }
      ],
      templates: [
        {
          name: "date",
          template: "{{= DateFormat.format.date(it, 'dd.MM.yyyy H:mm:ss') }}"
        },
        {
          name: "parentPageFieldText",
          template: "<span class='js-parent-page-field-text' data-parent-page-id='{{= it.parentId }}' data-parent-page-title='{{= it.parentTitle }}'>{{= it.parentTitle }} (ID: {{= it.parentId }})</span>"
        }
      ],
      actions: {
        getAction: {
          url: "/Admin/Pages/GetPagesOrderedByCreationDateDesc"
        },
        createAction: {
          url: "/Admin/Pages/CreatePage"
        },
        updateAction: {
          url: "/Admin/Pages/UpdatePage"
        },
        deleteAction: {
          url: "/Admin/Pages/DeletePage"
        }
      },
      minWidth: 700,
      height: 369,
      popups: {
        addItemPopup: {
          title: "Добавление новой страницы",
          content: $(".popups .js-add-page-popup").html()
        },
        editItemPopup: {
          title: "Редактирование страницы",
          content: $(".popups .js-edit-page-popup").html()
        },
        deleteItemPopup: {
          title: "Удаление страницы",
          content: $(".popups .js-delete-page-popup").html()
        }
      },
      texts: {
        addButtonTitle: "Добавить страницу",
        addButtonText: "<i class='fa fa-plus fa-lg'></i><span class='command-button-text'>Добавить страницу</span>",
        editButtonTitle: "Редактировать страницу",
        deleteButtonTitle: "Удалить страницу"
      }
    });
  }

  function createParentPagesDataTable($element) {
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
        },
        createAction: {
          url: "/Admin/Pages/CreatePage"
        }
      },
      minWidth: 700,
      height: 369,
      highlightLastClickedRow: true,
      addClickableRowsCssClass: true,
      popups: {
        addItemPopup: {
          title: "Добавление новой страницы",
          content: $(".popups .js-add-parent-page-popup").html()
        }
      },
      texts: {
        addButtonTitle: "Добавить страницу",
        addButtonText: "<i class='fa fa-plus fa-lg'></i><span class='command-button-text'>Добавить страницу</span>"
      },
      onRowClicked: onParentPagesDataTableRowClicked,
      onContentLoad: onParentPagesDataTableContentLoad,
      onItemCreated: onParentPagesDataTableItemCreated
    });
  }

  function resetParentPageSelection() {
    $parentPagesDataTable.data("dataTable").removeRowSelection();

    $selectedParentPageInfo
      .addClass("empty-value-text")
      .html(selectedParentPageInfoEmptyValueText);

    $selectedParentPageRemoveButton.css("display", "none");
  }

  function onParentPagesDataTableRowClicked(selectedItemId, $clickedRow) {
    if ($clickedRow.hasClass("disabled-row")) {
      selectedParentPageId = null;
      selectedParentPageTitle = null;

      resetParentPageSelection();
    } else {
      var selectedItem = _.find($parentPagesDataTable.data("dataTable").dataSource, function(x) {
        return x.id === selectedItemId;
      });

      selectedParentPageId = selectedItem["id"];
      selectedParentPageTitle = selectedItem["title"];

      $selectedParentPageInfo
        .removeClass("empty-value-text")
        .html(selectedParentPageInfoTemplateFunction({
          parentPageId: selectedItem["id"],
          parentPageTitle: selectedItem["title"]
        }));

      $selectedParentPageRemoveButton.css("display", "inline-block");
    }
  }

  function onParentPagesDataTableContentLoad() {
    $parentPagesDataTable.find("tbody tr").each(function() {
      var $tr = $(this);

      if ($tr.data("item-id") === selectedPageParentPageId) {
        $tr.addClass("disabled-row");
        $tr.prop("title", "Страница не может быть сама себе родителем");
        $tr.find("td").removeAttr("title");
      }
    });
  }

  function onParentPagesDataTableItemCreated() {
    var $pagesDataTablePaginator = $pagesDataTable.data("dataTable").paginator;

    $pagesDataTablePaginator.setCurrentPageNumber($pagesDataTablePaginator.settings.currentPageNumber);
  }

  function initializeEventHandlers() {
    $pagesDataTable.on("click", ".popup form [data-for-field='ParentId']", function() {
      var $parentPageField = $(this);
      var $parentIdFormGroup = $parentPageField.closest(".js-editor");
      var $selectParentPagePopup = $("<div></div>").appendTo($pagesDataTable).popup({
        title: "Выбор родительской страницы",
        content: $(".popups .js-select-parent-page-popup").html()
      });

      selectedPageParentPageId = parseInt($parentPageField.closest(".popup").find("#Id").val(), 10);
      $parentPagesDataTable = $selectParentPagePopup.find(".js-parent-pages-data-table");
      createParentPagesDataTable($parentPagesDataTable);

      $selectedParentPageInfo = $selectParentPagePopup.find(".js-selected-parent-page-info");
      $selectedParentPageRemoveButton = $selectParentPagePopup.find(".js-selected-parent-page-remove-button");

      var $parentPageFieldText = $parentPageField.children(".js-parent-page-field-text");

      if ($parentPageFieldText.length > 0) {
        $parentPagesDataTable.data("dataTable").selectRow($parentPageFieldText.data("parent-page-id"));

        selectedParentPageId = $parentPageFieldText.data("parent-page-id");
        selectedParentPageTitle = $parentPageFieldText.data("parent-page-title");

        $selectedParentPageInfo.html(selectedParentPageInfoTemplateFunction({
          parentPageId: selectedParentPageId,
          parentPageTitle: selectedParentPageTitle
        }));

        $selectedParentPageRemoveButton.css("display", "inline-block");
      } else {
        $selectedParentPageInfo
          .addClass("empty-value-text")
          .html(selectedParentPageInfoEmptyValueText);
      }

      $selectedParentPageRemoveButton.on("click", function() {
        selectedParentPageId = null;
        selectedParentPageTitle = null;

        resetParentPageSelection();
      });

      $selectParentPagePopup.on("click", ".js-command-buttons .save-button", function() {
        $pagesDataTable.data("dataTable").fillFormGroup($parentIdFormGroup, {
          parentId: selectedParentPageId,
          parentTitle: selectedParentPageTitle
        });

        selectedParentPageId = null;
        selectedParentPageTitle = null;

        $selectParentPagePopup.data("popup").destroyPopup();
      });

      $selectParentPagePopup.on("click", ".js-command-buttons .close-button", function() {
        selectedParentPageId = null;
        selectedParentPageTitle = null;

        $selectParentPagePopup.data("popup").destroyPopup();
      });
    });
  }

  $(function() {
    createPagesDataTable();
    initializeEventHandlers();
  });
})(jQuery, _);