(function($) {
  "use strict";

  function createMainMenuItemsDataTable() {
    $(".js-main-menu-items-data-table").dataTable({
      columns: [
        {
          field: "text",
          title: "Текст"
        },
        {
          field: "url",
          title: "Ссылка"
        },
        {
          field: "order",
          title: "Порядок",
          width: 100
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
        }
      ],
      actions: {
        getAction: {
          url: "/Admin/Main-Menu/GetMainMenuItemsOrderedByCreationDateDesc"
        },
        createAction: {
          url: "/Admin/Main-Menu/CreateMainMenuItem"
        },
        updateAction: {
          url: "/Admin/Main-Menu/UpdateMainMenuItem"
        },
        deleteAction: {
          url: "/Admin/Main-Menu/DeleteMainMenuItem"
        }
      },
      minWidth: 700,
      height: 369,
      popups: {
        addItemPopup: {
          title: "Добавление нового пункта главного меню",
          content: $(".popups .js-add-main-menu-item-popup").html()
        },
        editItemPopup: {
          title: "Редактирование пункта главного меню",
          content: $(".popups .js-edit-main-menu-item-popup").html()
        },
        deleteItemPopup: {
          title: "Удаление пункта главного меню",
          content: $(".popups .js-delete-main-menu-item-popup").html()
        }
      },
      texts: {
        addButtonTitle: "Добавить пункт главного меню",
        addButtonText: "<i class='fa fa-plus fa-lg'></i><span class='command-button-text'>Добавить пункт главного меню</span>",
        editButtonTitle: "Редактировать пункт главного меню",
        deleteButtonTitle: "Удалить пункт главного меню"
      }
    });
  }

  $(function() {
    createMainMenuItemsDataTable();
  });
})(jQuery);