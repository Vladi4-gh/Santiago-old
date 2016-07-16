(function($) {
  "use strict";

  function createTestimonialsDataTable() {
    $(".js-testimonials-data-table").dataTable({
      columns: [
        {
          field: "authorName",
          title: "Имя автора"
        },
        {
          field: "text",
          title: "Текст"
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
          url: "/Admin/Testimonials/GetTestimonialsOrderedByCreationDateDesc"
        },
        createAction: {
          url: "/Admin/Testimonials/CreateTestimonial"
        },
        updateAction: {
          url: "/Admin/Testimonials/UpdateTestimonial"
        },
        deleteAction: {
          url: "/Admin/Testimonials/DeleteTestimonial"
        }
      },
      minWidth: 700,
      height: 369,
      popups: {
        addItemPopup: {
          title: "Добавление нового отзыва",
          content: $(".popups .js-add-testimonial-popup").html(),
          onPopupOpen: function($popup) {
            var $testimonialAuthorImageFileUpload = $popup.element.find(".js-testimonial-author-image-file-upload");

            $testimonialAuthorImageFileUpload.fileUpload({
              fileInputName: "testimonialAuthorImageFile",
              fileInputAcceptedFileTypes: "image/jpeg,image/png",
              handlerUrl: "/FileUpload/UploadImageFile",
              onFileUploaded: function(data) {
                $testimonialAuthorImageFileUpload.prev("input[type='hidden']").val(JSON.parse(data).id);
              }
            });
          }
        },
        editItemPopup: {
          title: "Редактирование отзыва",
          content: $(".popups .js-edit-testimonial-popup").html(),
          onPopupOpen: function($popup) {
            var $testimonialAuthorImageFileUpload = $popup.element.find(".js-testimonial-author-image-file-upload");

            $testimonialAuthorImageFileUpload.fileUpload({
              fileInputName: "testimonialAuthorImageFile",
              fileInputAcceptedFileTypes: "image/jpeg,image/png",
              handlerUrl: "/FileUpload/UploadImageFile",
              onFileUploaded: function(data) {
                $testimonialAuthorImageFileUpload.prev("input[type='hidden']").val(JSON.parse(data).id);
              }
            });
          }
        },
        deleteItemPopup: {
          title: "Удаление отзыва",
          content: $(".popups .js-delete-testimonial-popup").html()
        }
      },
      texts: {
        addButtonTitle: "Добавить отзыв",
        addButtonText: "<i class='fa fa-plus fa-lg'></i><span class='command-button-text'>Добавить отзыв</span>",
        editButtonTitle: "Редактировать отзыв",
        deleteButtonTitle: "Удалить отзыв"
      }
    });
  }

  $(function() {
    createTestimonialsDataTable();
  });
})(jQuery);