(function($) {
  "use strict";

  function createPhotographsDataTable() {
    $(".js-photographs-data-table").dataTable({
      columns: [
        {
          field: "title",
          title: "Заголовок"
        },
        {
          field: "description",
          title: "Описание"
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
          url: "/Admin/Photographs/GetPhotographsOrderedByCreationDateDesc"
        },
        createAction: {
          url: "/Admin/Photographs/CreatePhotograph"
        },
        updateAction: {
          url: "/Admin/Photographs/UpdatePhotograph"
        },
        deleteAction: {
          url: "/Admin/Photographs/DeletePhotograph"
        }
      },
      minWidth: 700,
      height: 369,
      popups: {
        addItemPopup: {
          title: "Добавление новой фотографии",
          content: $(".popups .js-add-photograph-popup").html(),
          onPopupOpen: function($popup) {
            var $photographGalleryItemImageFileUpload = $popup.element.find(".js-photograph-gallery-item-image-file-upload");

            $photographGalleryItemImageFileUpload.fileUpload({
              fileInputName: "photographGalleryItemImageFile",
              fileInputAcceptedFileTypes: "image/jpeg,image/png",
              handlerUrl: "/FileUpload/UploadImageFile",
              onFileUploaded: function(data) {
                $photographGalleryItemImageFileUpload.prev("input[type='hidden']").val(JSON.parse(data).id);
              }
            });

            var $photographGallerySliderImageFileUpload = $popup.element.find(".js-photograph-gallery-slider-image-file-upload");

            $photographGallerySliderImageFileUpload.fileUpload({
              fileInputName: "photographGallerySliderImageFile",
              fileInputAcceptedFileTypes: "image/jpeg,image/png",
              handlerUrl: "/FileUpload/UploadImageFile",
              onFileUploaded: function(data) {
                $photographGallerySliderImageFileUpload.prev("input[type='hidden']").val(JSON.parse(data).id);
              }
            });
          }
        },
        editItemPopup: {
          title: "Редактирование фотографии",
          content: $(".popups .js-edit-photograph-popup").html(),
          onPopupOpen: function($popup) {
            var $photographGalleryItemImageFileUpload = $popup.element.find(".js-photograph-gallery-item-image-file-upload");

            $photographGalleryItemImageFileUpload.fileUpload({
              fileInputName: "photographGalleryItemImageFile",
              fileInputAcceptedFileTypes: "image/jpeg,image/png",
              handlerUrl: "/FileUpload/UploadImageFile",
              onFileUploaded: function(data) {
                $photographGalleryItemImageFileUpload.prev("input[type='hidden']").val(JSON.parse(data).id);
              }
            });

            var $photographGallerySliderImageFileUpload = $popup.element.find(".js-photograph-gallery-slider-image-file-upload");

            $photographGallerySliderImageFileUpload.fileUpload({
              fileInputName: "photographGallerySliderImageFile",
              fileInputAcceptedFileTypes: "image/jpeg,image/png",
              handlerUrl: "/FileUpload/UploadImageFile",
              onFileUploaded: function(data) {
                $photographGallerySliderImageFileUpload.prev("input[type='hidden']").val(JSON.parse(data).id);
              }
            });
          }
        },
        deleteItemPopup: {
          title: "Удаление фотографии",
          content: $(".popups .js-delete-photograph-popup").html()
        }
      },
      texts: {
        addButtonTitle: "Добавить фотографию",
        addButtonText: "<i class='fa fa-plus fa-lg'></i><span class='command-button-text'>Добавить фотографию</span>",
        editButtonTitle: "Редактировать фотографию",
        deleteButtonTitle: "Удалить фотографию"
      }
    });
  }

  $(function() {
    createPhotographsDataTable();
  });
})(jQuery);