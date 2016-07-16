(function($) {
  "use strict";

  function createPhotographCategoriesDataTable() {
    $(".js-photograph-categories-data-table").dataTable({
      columns: [
        {
          field: "name",
          title: "Название"
        },
        {
          field: "alias",
          title: "Псевдоним"
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
          url: "/Admin/Photograph-Categories/GetPhotographCategoriesOrderedByCreationDateDesc"
        },
        createAction: {
          url: "/Admin/Photograph-Categories/CreatePhotographCategory"
        },
        updateAction: {
          url: "/Admin/Photograph-Categories/UpdatePhotographCategory"
        },
        deleteAction: {
          url: "/Admin/Photograph-Categories/DeletePhotographCategory"
        }
      },
      minWidth: 700,
      height: 369,
      popups: {
        addItemPopup: {
          title: "Добавление новой категории",
          content: $(".popups .js-add-photograph-category-popup").html()
        },
        editItemPopup: {
          title: "Редактирование категории",
          content: $(".popups .js-edit-photograph-category-popup").html()
        },
        deleteItemPopup: {
          title: "Удаление категории",
          content: $(".popups .js-delete-photograph-category-popup").html()
        }
      },
      texts: {
        addButtonTitle: "Добавить категорию",
        addButtonText: "<i class='fa fa-plus fa-lg'></i><span class='command-button-text'>Добавить категорию</span>",
        editButtonTitle: "Редактировать категорию",
        deleteButtonTitle: "Удалить категорию"
      }
    });
  }

  $(function() {
    createPhotographCategoriesDataTable();
  });
})(jQuery);