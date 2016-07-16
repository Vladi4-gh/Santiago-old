(function($) {
  "use strict";

  $.fileUpload = function(element, options) {
    var defaults = {
      additionalCssClass: "",
      fileInputName: "file",
      fileInputAcceptedFileTypes: "*",
      handlerUrl: "",
      uploadButtonText: "Выбрать файл",
      onFileUploaded: function(data) { }
    };

    var fileUpload = this;

    function createFileUpload() {
      fileUpload.settings = $.extend(true, defaults, options);
      fileUpload.element = $(element);
      fileUpload.fileInput = $("<input type='file' name='" + fileUpload.settings.fileInputName + "' accept='" + fileUpload.settings.fileInputAcceptedFileTypes + "'>");
      fileUpload.uploadButton = $("<button class='upload-button js-upload-button' type='button'>" + fileUpload.settings.uploadButtonText + "</button>");
      fileUpload.uploadStateMessage = $("<span class='upload-state-message js-upload-state-message'></span>");

      fileUpload.element
        .addClass("file-upload " + fileUpload.settings.additionalCssClass)
        .append(fileUpload.fileInput)
        .append(fileUpload.uploadButton)
        .append(fileUpload.uploadStateMessage);
    }

    function initializeEventHandlers() {
      fileUpload.uploadButton.on("click", function() {
        fileUpload.fileInput.click();
      });

      fileUpload.fileInput.on("change", function() {
        var formData = new FormData();

        formData.append("file", $(this).prop("files")[0]);

        $.ajax({
          url: fileUpload.settings.handlerUrl,
          type: "POST",
          dataType: "json",
          cache: false,
          contentType: false,
          processData: false,
          data: formData,
          beforeSend: function() {
            fileUpload.uploadStateMessage.text("Файл загружается");
          },
          success: function(data) {
            console.log(JSON.parse(data));

            fileUpload.uploadStateMessage.text("Файл успешно загружен");

            fileUpload.settings.onFileUploaded(data);
          },
          error: function() {
            fileUpload.uploadStateMessage.text("При загрузке файла произошла ошибка!");
          }
        });

        fileUpload.fileInput.val("");
      });
    }

    (function() {
      createFileUpload();
      initializeEventHandlers();
    })();
  };

  $.fn.fileUpload = function(options) {
    return this.each(function() {
      var $this = $(this);

      if (!$this.data("fileUpload")) {
        $this.data("fileUpload", new $.fileUpload(this, options));
      }
    });
  };
})(jQuery);