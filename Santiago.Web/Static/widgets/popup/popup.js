(function($) {
  "use strict";

  $.popup = function(element, options) {
    var defaults = {
      additionalCssClass: "",
      title: "Заголовок",
      content: "Содержимое",
      closePopupOnWrapperClick: false,
      onPopupOpen: function() { }
    };

    var popup = this;

    popup.destroyPopup = destroyPopup;

    function createPopup() {
      popup.settings = $.extend(true, defaults, options);
      popup.element = $(element);

      disableBodyScrollingIfNeeded();

      popup.element
        .addClass("popup " + popup.settings.additionalCssClass)
        .wrap(createPopupUnderlay())
        .append(createPopupHeader(), createPopupContent());

      popup.settings.onPopupOpen(popup);
    }

    function createPopupUnderlay() {
      return "<div class='popup-underlay'></div>";
    }

    function createPopupHeader() {
      return "<div class='popup-header clearfix'>" +
        "<div class='heading-wrapper'><h4>" + popup.settings.title + "</h4></div>" +
        "<div class='command-button-wrapper'><button type='button' class='command-button close-button'><i class='fa fa-times fa-lg'></i></button></div>" +
        "</div>";
    }

    function createPopupContent() {
      return $("<div class='popup-content'></div>").append(popup.settings.content);
    }

    function disableBodyScrollingIfNeeded() {
      var $body = $("body");
      var dataPopupNestingLevel = $body.data("popupNestingLevel");
      var popupNestingLevel = dataPopupNestingLevel ? dataPopupNestingLevel : 0;

      if (popupNestingLevel === 0) {
        $body
          .css("overflow", "hidden")
          .data("popupNestingLevel", 1);
      } else {
        $body.data("popupNestingLevel", ++popupNestingLevel);
      }
    }

    function enableBodyScrollingIfNeeded() {
      var $body = $("body");
      var popupNestingLevel = $body.data("popupNestingLevel");

      if (popupNestingLevel > 0) {
        $body.data("popupNestingLevel", --popupNestingLevel);
      }

      if (popupNestingLevel === 0) {
        $body
          .css("overflow", "auto")
          .removeData("popupNestingLevel");
      }
    }

    function destroyPopup() {
      enableBodyScrollingIfNeeded();

      popup.element.parent(".popup-underlay").remove();
    }

    function initializeEventHandlers() {
      popup.element.closest(".popup-underlay").on("click", function(e) {
        if (popup.settings.closePopupOnWrapperClick) {
          if (e.target !== this) {
            return;
          }

          destroyPopup();
        }
      });

      popup.element.find(".popup-header .close-button").on("click", function() {
        destroyPopup();
      });
    }

    (function() {
      createPopup();
      initializeEventHandlers();
    })();
  };

  $.fn.popup = function(options) {
    return this.each(function() {
      var $this = $(this);

      if (!$this.data("popup")) {
        $this.data("popup", new $.popup(this, options));
      }
    });
  };
})(jQuery);