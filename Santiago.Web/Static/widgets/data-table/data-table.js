(function($, _) {
  "use strict";

  $.dataTable = function(element, options) {
    var defaults = {
      additionalCssClass: "",
      columns: [],
      actions: {
        getAction: {
          type: "GET",
          url: ""
        },
        createAction: {
          type: "POST",
          url: ""
        },
        updateAction: {
          type: "POST",
          url: ""
        },
        deleteAction: {
          type: "POST",
          url: ""
        }
      },
      modelIdField: "id",
      pageSize: 10,
      pageable: true,
      editable: true,
      highlightLastClickedRow: false,
      addClickableRowsCssClass: false,
      currentSelectedItemId: null,
      popups: {
        addItemPopup: {
          title: "Добавление записи",
          content: "Здесь должна быть форма добавления записи",
          onPopupOpen: function() { }
        },
        editItemPopup: {
          title: "Редактирование записи",
          content: "Здесь должна быть форма редактирования записи",
          onPopupOpen: function() { }
        },
        deleteItemPopup: {
          title: "Удаление записи",
          content: "Здесь должна быть форма удаления записи",
          onPopupOpen: function() { }
        }
      },
      texts: {
        addButtonTitle: "Добавить запись",
        addButtonText: "<i class='fa fa-plus fa-lg'></i><span class='command-button-text'>Добавить запись</span>",
        editButtonTitle: "Редактировать запись",
        editButtonText: "<i class='fa fa-pencil fa-lg'></i>",
        deleteButtonTitle: "Удалить запись",
        deleteButtonText: "<i class='fa fa-trash fa-lg'></i>"
      },
      onRowClicked: function() { },
      onContentLoad: function() { },
      onItemCreated: function() { },
      onItemUpdated: function() { },
      onItemDeleted: function() { }
    };

    var dataTable = this;
    var $dataTableHeader;
    var $dataTableHeaderToolbarWrapper;
    var $dataTableHeaderTable;
    var $dataTableHeaderTableWrapper;
    var $dataTableContent;
    var $dataTableContentTable;
    var $dataTableContentTableBody;
    var $dataTableContentTableWrapper;
    var $dataTableContentLoadingOverlay;

    dataTable.fillFormGroup = fillFormGroup;
    dataTable.selectRow = selectRow;
    dataTable.removeRowSelection = removeRowSelection;

    function createDataTable() {
      dataTable.settings = $.extend(true, defaults, options);
      dataTable.element = $(element);

      dataTable.element
        .addClass("data-table " + dataTable.settings.additionalCssClass)
        .css("max-width", dataTable.settings.maxWidth)
        .append(createDataTableHeader(), createDataTableContent(), createDataTableFooter());

      $dataTableHeaderTableWrapper.css("margin-right", ($dataTableContent.width() - $dataTableContentTableWrapper.width()) - 1);
    }

    function createColgroup() {
      var $colGroup = $("<colgroup></colgroup>");

      _.forEach(dataTable.settings.columns, function(x) {
        if (x.commands && !dataTable.settings.editable) {
          return;
        }

        $colGroup.append($("<col>").css("width", x.width));
      });

      return $colGroup;
    }

    function createDataTableHeader() {
      $dataTableHeaderToolbarWrapper = $("<div class='data-table-header-toolbar-wrapper'></div>").append(createDataTableToolbar());
      $dataTableHeaderTableWrapper = $("<div class='data-table-header-table-wrapper'></div>").append(createDataTableHead());
      $dataTableHeader = $("<div class='data-table-header'></div>")
        .append($dataTableHeaderToolbarWrapper)
        .append($dataTableHeaderTableWrapper);

      return $dataTableHeader;
    }

    function createDataTableToolbar() {
      return dataTable.settings.editable ? "<button type='button' class='command-button add-button' title='" + dataTable.settings.texts.addButtonTitle + "'>" + dataTable.settings.texts.addButtonText + "</button>" : "";
    }

    function createDataTableHead() {
      $dataTableHeaderTable = $("<table></table>")
        .css({
          "width": dataTable.settings.width,
          "min-width": dataTable.settings.minWidth
        })
        .append(createColgroup())
        .append(createTableHead());

      return $dataTableHeaderTable;
    }

    function createTableHead() {
      var titles = "";

      _.forEach(dataTable.settings.columns, function(x) {
        if (x.commands && !dataTable.settings.editable) {
          return;
        }

        titles += "<th>" + (x.title ? x.title : "") + "</th>";
      });

      return "<thead><tr>" + titles + "</tr></thead>";
    }

    function createDataTableContent() {
      $dataTableContentTableWrapper = $("<div class='data-table-content-table-wrapper'></div>").append(createDataTableBody());
      $dataTableContent = $("<div class='data-table-content'></div>")
        .css({
          "height": dataTable.settings.height,
          "min-height": dataTable.settings.minHeight,
          "max-height": dataTable.settings.maxHeight
        })
        .append(createDataTableContentLoadingOverlay())
        .append($dataTableContentTableWrapper);

      return $dataTableContent;
    }

    function createDataTableContentLoadingOverlay() {
      $dataTableContentLoadingOverlay = $("<div class='data-table-content-loading-overlay'><i class='fa fa-circle-o-notch fa-spin fa-4x fa-fw'></i></div>");

      return $dataTableContentLoadingOverlay;
    }

    function createDataTableBody() {
      $dataTableContentTableBody = $("<tbody></tbody>");
      $dataTableContentTable = $("<table></table>")
        .css({
          "width": dataTable.settings.width,
          "min-width": dataTable.settings.minWidth
        })
        .append(createColgroup())
        .append($dataTableContentTableBody);

      if (dataTable.settings.addClickableRowsCssClass) {
        $dataTableContentTable.addClass("clickable-rows");
      }

      return $dataTableContentTable;
    }

    function createDataTableFooter() {
      if (dataTable.settings.pageable) {
        var $paginator = $("<div></div>").paginator({
          onPageNumberChanged: function(pageNumber) {
            refreshDataTable(pageNumber);
          }
        });

        dataTable.paginator = $paginator.data("paginator");

        return $("<div class='data-table-footer'></div>").append($paginator);
      }

      return "";
    }

    function disableDataTable() {
      $dataTableHeader.find(".command-button")
        .addClass("disabled")
        .prop("disabled", true);

      $dataTableContentTableBody.find(".command-button")
        .addClass("disabled")
        .prop("disabled", true);

      dataTable.paginator.disablePaginator();
    }

    function enableDataTable() {
      $dataTableHeader.find(".command-button")
        .removeClass("disabled")
        .prop("disabled", false);

      $dataTableContentTableBody.find(".command-button")
        .removeClass("disabled")
        .prop("disabled", false);

      dataTable.paginator.enablePaginator();
    }

    function refreshDataTable(pageNumber) {
      var currentPageNumber = pageNumber ? pageNumber : dataTable.paginator.settings.currentPageNumber;

      getItems((currentPageNumber - 1) * dataTable.settings.pageSize);
    }

    function getItems(skipNumber) {
      $.ajax({
        type: dataTable.settings.actions.getAction.type,
        url: dataTable.settings.actions.getAction.url,
        data: {
          skipNumber: skipNumber,
          takeNumber: dataTable.settings.pageSize
        },
        beforeSend: function() {
          $dataTableContentLoadingOverlay.css("display", "flex");
          disableDataTable();
        },
        success: function(data) {
          var receivedData = JSON.parse(data);

          emptyDataSource();
          emptyDataTable();

          if (receivedData["itemsTotalCount"] && receivedData["itemsTotalCount"] !== 0) {
            setPaginatorState(receivedData["itemsTotalCount"]);
          }

          if (receivedData["items"] && receivedData["items"].length !== 0) {
            fillDataSource(receivedData["items"]);
            fillDataTableBody(receivedData["items"]);
          }
        },
        error: function() {
          alert("Произошла неизвестная ошибка!");
        },
        complete: function() {
          $dataTableContentLoadingOverlay.css("display", "none");
          enableDataTable();
          dataTable.settings.onContentLoad();
        }
      });
    }

    function emptyDataTable() {
      $dataTableContentTableBody.empty();
    }

    function emptyDataSource() {
      dataTable.dataSource = [];
    }

    function fillDataSource(data) {
      dataTable.dataSource = data;
    }

    function setPaginatorState(itemsTotalCount) {
      dataTable.paginator.setMaxPageNumber(Math.ceil(itemsTotalCount / dataTable.settings.pageSize));

      if (dataTable.paginator.currentPageNumber > dataTable.paginator.maxPageNumber) {
        dataTable.paginator.currentPageNumber = dataTable.paginator.maxPageNumber;
      }
    }

    function fillDataTableBody(data) {
      var rows = [];

      _.forEach(data, function(x) {
        var $row = $("<tr></tr>");

        if (dataTable.settings.currentSelectedItemId === x[dataTable.settings.modelIdField]) {
          $row.addClass("highlighted");
        }

        $row.data("item-id", x[dataTable.settings.modelIdField]);

        var rowCells = [];

        _.forEach(dataTable.settings.columns, function(y) {
          if (y.commands) {
            if (dataTable.settings.editable) {
              rowCells.push($("<td class='command-buttons-wrapper'>" + addCommandButtons(y.commands) + "</td>"));
            }

            return;
          }

          var cellValue;
          var cellTitle;

          if (x[y.field] === undefined || x[y.field] === null) {
            cellValue = "<span class='empty-value-text'>-</span>";
            cellTitle = "Нет значения";
          } else if (y.templateName) {
            cellValue = renderTemplate(y.templateName, x[y.field]);
            cellTitle = cellValue;
          } else {
            cellValue = x[y.field];
            cellTitle = cellValue;
          }

          var $cell = $("<td class='text-cell' title='" + cellTitle + "'></td>");

          if (y.isHtml) {
            $cell.html(cellValue);
          } else {
            $cell.text(cellValue);
          }

          rowCells.push($cell);
        });

        rows.push($row.append(rowCells));
      });

      if (rows.length < dataTable.settings.pageSize) {
        rows[rows.length - 1].addClass("bottom-border");
      }

      $dataTableContentTableBody.append(rows);
    }

    function addCommandButtons(commands) {
      var commandButtons = "";

      _.forEach(commands, function(x) {
        if (x === "edit") {
          commandButtons += "<button type='button' class='command-button edit-button' title='" + dataTable.settings.texts.editButtonTitle + "'>" + dataTable.settings.texts.editButtonText + "</button>";
        } else if (x === "delete") {
          commandButtons += "<button type='button' class='command-button delete-button' title='" + dataTable.settings.texts.deleteButtonTitle + "'>" + dataTable.settings.texts.deleteButtonText + "</button>";
        }
      });

      return commandButtons;
    }

    function renderTemplate(templateName, value) {
      var template = _.find(dataTable.settings.templates, function(x) {
        return x.name === templateName;
      });

      if (template) {
        var templateFunction = doT.template(template.template);

        return templateFunction(value);
      }

      return value;
    }

    function fillForm($form, dataSourceItem) {
      $form.find(".js-editor").each(function() {
        fillFormGroup($(this), dataSourceItem);
      });
    }

    function fillFormGroup($formGroup, dataSourceItem) {
      var fieldValue = dataSourceItem[$formGroup.data("editor-name")];
      var templateName;

      switch ($formGroup.data("editor-type")) {
        case "popup-display-value":
          var $displayValue = $formGroup.find("span.display-value");

          if (fieldValue !== undefined && fieldValue !== null) {
            $displayValue.removeClass("empty-value-text");
            templateName = $displayValue.data("template-name");

            if (templateName) {
              $displayValue.html(renderTemplate(templateName, fieldValue));
            } else {
              $displayValue.html(fieldValue);
            }
          } else {
            $displayValue
              .addClass("empty-value-text")
              .html($displayValue.data("empty-value"));
          }

          break;
        case "popup-dropdownlist":
          var $dropdownlist = $formGroup.find("select");

          if (fieldValue !== undefined && fieldValue !== null) {
            if (_.isBoolean(fieldValue)) {
              if (fieldValue === true) {
                $dropdownlist.val("true");
              } else {
                $dropdownlist.val("false");
              }
            } else {
              $dropdownlist.val(fieldValue);
            }
          } else {
            $dropdownlist.val($dropdownlist.find("option:first-child").val());
          }

          break;
        case "popup-file-upload":
          var $uploadedFileIdHidden = $formGroup.find("input[type='hidden'].js-uploaded-file-id");

          if (fieldValue !== undefined && fieldValue !== null) {
            $uploadedFileIdHidden.val(fieldValue);
          } else {
            $uploadedFileIdHidden.val("");
          }

          break;
        case "popup-hidden-with-display-value":
          var $hidden = $formGroup.find("input[type='hidden']");
          var $hiddenDisplayValue = $formGroup.find("span.display-value");

          if (fieldValue !== undefined && fieldValue !== null) {
            $hidden.val(fieldValue);
            $hiddenDisplayValue.removeClass("empty-value-text");
            templateName = $hiddenDisplayValue.data("template-name");

            if (templateName) {
              $hiddenDisplayValue.html(renderTemplate(templateName, dataSourceItem));
            } else {
              $hiddenDisplayValue.html(fieldValue);
            }
          } else {
            $hidden.val("");
            $hiddenDisplayValue
              .addClass("empty-value-text")
              .html($hiddenDisplayValue.data("empty-value"));
          }

          break;
        case "popup-textarea":
          var $textarea = $formGroup.find("textarea");

          if (fieldValue !== undefined && fieldValue !== null) {
            $textarea.val(fieldValue);
          } else {
            $textarea.val("");
          }

          break;
        case "popup-textbox":
          var $textbox = $formGroup.find("input[type='text']");

          if (fieldValue !== undefined && fieldValue !== null) {
            $textbox.val(fieldValue);
          } else {
            $textbox.val("");
          }

          break;
      }
    }

    function createItem(itemData, $addItemPopup, $addItemPopupForm) {
      $.ajax({
        type: dataTable.settings.actions.createAction.type,
        url: dataTable.settings.actions.createAction.url,
        data: itemData,
        success: function(serverValidationErrorsObject) {
          if (serverValidationErrorsObject) {
            $addItemPopupForm.validate().showErrors(JSON.parse(serverValidationErrorsObject));
          } else {
            dataTable.settings.onItemCreated();
            $addItemPopup.data("popup").destroyPopup();
            refreshDataTable();
          }
        },
        error: function() {
          alert("Произошла неизвестная ошибка!");
        }
      });
    }

    function updateItem(itemData, $editItemPopup, $editItemPopupForm) {
      $.ajax({
        type: dataTable.settings.actions.updateAction.type,
        url: dataTable.settings.actions.updateAction.url,
        data: itemData,
        success: function(serverValidationErrorsObject) {
          if (serverValidationErrorsObject) {
            $editItemPopupForm.validate().showErrors(JSON.parse(serverValidationErrorsObject));
          } else {
            dataTable.settings.onItemUpdated();
            $editItemPopup.data("popup").destroyPopup();
            refreshDataTable();
          }
        },
        error: function() {
          alert("Произошла неизвестная ошибка!");
        }
      });
    }

    function deleteItem(itemId, $deleteItemPopup) {
      $.ajax({
        type: dataTable.settings.actions.deleteAction.type,
        url: dataTable.settings.actions.deleteAction.url,
        data: {
          id: itemId
        },
        success: function() {
          dataTable.settings.onItemDeleted();
          $deleteItemPopup.data("popup").destroyPopup();
          refreshDataTable();
        },
        error: function() {
          alert("Произошла неизвестная ошибка!");
        }
      });
    }

    function selectRow(dataSourceItemId) {
      dataTable.settings.currentSelectedItemId = dataSourceItemId;

      $dataTableContentTableBody.find("tr.highlighted").removeClass("highlighted");

      $dataTableContentTableBody.find("tr").each(function() {
        var $this = $(this);

        if ($this.data("item-id") === dataSourceItemId) {
          $this.addClass("highlighted");
        }
      });
    }

    function removeRowSelection() {
      dataTable.settings.currentSelectedItemId = null;

      $dataTableContentTableBody.find("tr.highlighted").removeClass("highlighted");
    }

    function initializeEventHandlers() {
      $dataTableContent.on("scroll", function() {
        var dataTableContentTablePosition = $dataTableContentTable.position();

        $dataTableHeaderTable.css("left", dataTableContentTablePosition.left);

        $dataTableContentLoadingOverlay.css({
          "left": dataTableContentTablePosition.left * -1,
          "top": dataTableContentTablePosition.top * -1
        });
      });

      $dataTableContentTableBody.on("click", "tr", function() {
        if (dataTable.settings.highlightLastClickedRow) {
          $dataTableContentTableBody.find("tr.highlighted").removeClass("highlighted");

          var $clickedRow = $(this);

          $clickedRow.addClass("highlighted");

          dataTable.settings.currentSelectedItemId = $clickedRow.data("item-id");
          dataTable.settings.onRowClicked(dataTable.settings.currentSelectedItemId, $clickedRow);
        }
      });

      if (dataTable.settings.editable) {
        $dataTableHeader.on("click", ".add-button", function() {
          var $addItemPopup = $("<div></div>").appendTo(dataTable.element).popup({
            title: dataTable.settings.popups.addItemPopup.title,
            content: dataTable.settings.popups.addItemPopup.content,
            onPopupOpen: dataTable.settings.popups.addItemPopup.onPopupOpen
          });

          var $addItemPopupForm = $addItemPopup.find("form");

          $addItemPopupForm.find(":input:visible:enabled:first").focus();
          $.validator.unobtrusive.parse($addItemPopupForm);

          $addItemPopupForm.on("submit", function(e) {
            e.preventDefault();

            if ($(this).valid()) {
              createItem($(this).serialize(), $addItemPopup, $addItemPopupForm);
            }
          });

          $addItemPopupForm.on("click", ".close-button", function() {
            $addItemPopup.data("popup").destroyPopup();
          });
        });

        $dataTableContentTableBody.on("click", ".edit-button", function() {
          var $editItemPopup = $("<div></div>").appendTo(dataTable.element).popup({
            title: dataTable.settings.popups.editItemPopup.title,
            content: dataTable.settings.popups.editItemPopup.content,
            onPopupOpen: dataTable.settings.popups.editItemPopup.onPopupOpen
          });

          var $editItemPopupForm = $editItemPopup.find("form");
          var dataSourceItemId = $(this).closest("tr").data("item-id");
          var dataSourceItem = _.find(dataTable.dataSource, function(x) {
            return x[dataTable.settings.modelIdField] === dataSourceItemId;
          });

          $editItemPopupForm.find(":input:visible:enabled:first").focus();
          $.validator.unobtrusive.parse($editItemPopupForm);
          fillForm($editItemPopupForm, dataSourceItem);

          $editItemPopupForm.on("submit", function(e) {
            e.preventDefault();

            if ($(this).valid()) {
              updateItem($(this).serialize(), $editItemPopup, $editItemPopupForm);
            }
          });

          $editItemPopupForm.on("click", ".close-button", function() {
            $editItemPopup.data("popup").destroyPopup();
          });
        });

        $dataTableContentTableBody.on("click", ".delete-button", function() {
          var $deleteItemPopup = $("<div></div>").appendTo(dataTable.element).popup({
            title: dataTable.settings.popups.deleteItemPopup.title,
            content: dataTable.settings.popups.deleteItemPopup.content,
            onPopupOpen: dataTable.settings.popups.deleteItemPopup.onPopupOpen
          });

          var dataSourceItemId = $(this).closest("tr").data("item-id");

          $deleteItemPopup.on("click", ".popup-content .yes-button", function() {
            deleteItem(dataSourceItemId, $deleteItemPopup);
          });

          $deleteItemPopup.on("click", ".popup-content .no-button", function() {
            $deleteItemPopup.data("popup").destroyPopup();
          });
        });
      }
    }

    (function() {
      createDataTable();
      initializeEventHandlers();

      getItems(0);
    })();
  };

  $.fn.dataTable = function(options) {
    return this.each(function() {
      var $this = $(this);

      if (!$this.data("dataTable")) {
        $this.data("dataTable", new $.dataTable(this, options));
      }
    });
  };
})(jQuery, _);