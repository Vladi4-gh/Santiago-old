(function($, _, window) {
  "use strict";

  $.photographGallerySlider = function(element, options) {
    var defaults = {
      additionalCssClass: "",
      closePhotographGallerySliderOnPhotographContainerClick: false,
      texts: {
        openPhotographInNewTabButtonTitle: "Открыть фотографию в новой вкладке",
        closeSliderButtonTitle: "Закрыть",
        previousPhotographButtonTitle: "Предыдущая фотография",
        nextPhotographButtonTitle: "Следующая фотография"
      }
    };

    var photographGallerySlider = this;
    var $photographGallerySliderPhotographContainer;
    var $photographGallerySliderPhotographLoadingSpinner = $(
      "<div class='photograph-loading-spinner-container'>" +
        "<i class='fa fa-circle-o-notch fa-spin fa-4x fa-fw'></i>" +
      "</div>");
    var $photographGallerySliderOpenPhotographInNewTabButton;
    var $photographGallerySliderCloseSliderButton;
    var $photographGallerySliderPreviousPhotographButton;
    var $photographGallerySliderNextPhotographButton;

    var $window = $(window);
    var photographGallerySliderPhotographContainerWidth = 0;
    var photographGallerySliderPhotographContainerHeight = 0;

    photographGallerySlider.currentGalleryItemIndex = null;

    photographGallerySlider.showPhotograph = showPhotograph;

    function createPhotographGallerySlider() {
      photographGallerySlider.settings = $.extend(true, defaults, options);
      photographGallerySlider.element = $(element);

      photographGallerySlider.element
        .addClass("photograph-gallery-slider " + photographGallerySlider.settings.additionalCssClass)
        .css({
          display: "none"
        })
        .append(createPhotographGallerySliderOpenPhotographInNewTabButton())
        .append(createPhotographGallerySliderCloseSliderButton())
        .append(createPhotographGallerySliderPreviousPhotographButton())
        .append(createPhotographGallerySliderPhotographContainer())
        .append(createPhotographGallerySliderNextPhotographButton());
    }

    function createPhotographGallerySliderPhotographContainer() {
      $photographGallerySliderPhotographContainer = $("<div class='photograph-container'></div>");

      return $photographGallerySliderPhotographContainer;
    }

    function createPhotographGallerySliderOpenPhotographInNewTabButton() {
      $photographGallerySliderOpenPhotographInNewTabButton = $(
        "<a class='slider-button slider-top-button open-photograph-in-new-tab-button' target='_blank' title='" + photographGallerySlider.settings.texts.openPhotographInNewTabButtonTitle + "'>" +
          "<i class='fa fa-external-link fa-2x'></i>" +
        "</a>");

      return $photographGallerySliderOpenPhotographInNewTabButton;
    }

    function createPhotographGallerySliderCloseSliderButton() {
      $photographGallerySliderCloseSliderButton = $(
        "<button type='button' class='slider-button slider-top-button close-slider-button' title='" + photographGallerySlider.settings.texts.closeSliderButtonTitle + "'>" +
          "<i class='fa fa-times fa-2x'></i>" +
        "</button>");

      return $photographGallerySliderCloseSliderButton;
    }

    function createPhotographGallerySliderPreviousPhotographButton() {
      $photographGallerySliderPreviousPhotographButton = $(
        "<button type='button' class='slider-button slider-navigation-button previous-photograph-button' title='" + photographGallerySlider.settings.texts.previousPhotographButtonTitle + "'>" +
          "<i class='fa fa-angle-left fa-4x'></i>" +
        "</button>");

      return $photographGallerySliderPreviousPhotographButton;
    }

    function createPhotographGallerySliderNextPhotographButton() {
      $photographGallerySliderNextPhotographButton = $(
        "<button type='button' class='slider-button slider-navigation-button next-photograph-button' title='" + photographGallerySlider.settings.texts.nextPhotographButtonTitle + "'>" +
          "<i class='fa fa-angle-right fa-4x'></i>" +
        "</button>");

      return $photographGallerySliderNextPhotographButton;
    }

    function openPhotographGallerySlider() {
      photographGallerySlider.element.css({
        display: ""
      });

      $("body").css({
        overflow: "hidden"
      });

      photographGallerySliderPhotographContainerWidth = $photographGallerySliderPhotographContainer.width();
      photographGallerySliderPhotographContainerHeight = $photographGallerySliderPhotographContainer.height();
    }

    function closePhotographGallerySlider() {
      $("body").css({
        overflow: ""
      });

      photographGallerySlider.element.css({
        display: "none"
      });

      $photographGallerySliderPhotographContainer.find(".photograph-wrapper").remove();
      $photographGallerySliderOpenPhotographInNewTabButton.removeAttr("href");

      photographGallerySlider.currentGalleryItemIndex = null;

      photographGallerySliderPhotographContainerWidth = 0;
      photographGallerySliderPhotographContainerHeight = 0;
    }

    function calculatePhotographWrapperSize(gallerySliderImageFileWidth, gallerySliderImageFileHeight) {
      var ratio = Math.min(photographGallerySliderPhotographContainerWidth / gallerySliderImageFileWidth, photographGallerySliderPhotographContainerHeight / gallerySliderImageFileHeight);

      return {
        width: Math.ceil(gallerySliderImageFileWidth * ratio),
        height: Math.ceil(gallerySliderImageFileHeight * ratio)
      };
    }

    function openPhotograph(galleryItemIndex) {
      if (galleryItemIndex === -1) {
        console.error("Произошла ошибка при открытии фотографии в слайдере!");
      } else {
        if (galleryItemIndex === 0) {
          $photographGallerySliderPreviousPhotographButton.css({
            visibility: "hidden"
          });

          $photographGallerySliderNextPhotographButton.css({
            visibility: "visible"
          });
        } else if (galleryItemIndex === photographGallerySlider.gallery.galleryItems.length - 1) {
          $photographGallerySliderPreviousPhotographButton.css({
            visibility: "visible"
          });

          $photographGallerySliderNextPhotographButton.css({
            visibility: "hidden"
          });
        } else {
          $photographGallerySliderPreviousPhotographButton.css({
            visibility: "visible"
          });

          $photographGallerySliderNextPhotographButton.css({
            visibility: "visible"
          });
        }

        var galleryItemData = photographGallerySlider.gallery.galleryItems[galleryItemIndex].itemData;
        var photographWrapperSize = calculatePhotographWrapperSize(galleryItemData.gallerySliderImageFile.width, galleryItemData.gallerySliderImageFile.height);
        var $photographWrapper = $photographGallerySliderPhotographContainer.find(".photograph-wrapper");
        var $photograph = $("<img class='photograph' style='display: none;'>")
          .attr("src", galleryItemData.gallerySliderImageFile.url)
          .attr("width", galleryItemData.gallerySliderImageFile.width)
          .attr("height", galleryItemData.gallerySliderImageFile.height)
          .attr("alt", galleryItemData.description)
          .one("load", function() {
            var $photograph = $(this);

            $photograph.siblings(".photograph-loading-spinner-container").remove();
            $photograph.show();
          });

        if ($photographWrapper.length === 0) {
          $photographWrapper = $("<div class='photograph-wrapper'></div>")
            .append($photographGallerySliderPhotographLoadingSpinner)
            .css({
              width: photographWrapperSize.width,
              height: photographWrapperSize.height
            })
            .append($photograph);

          $photographWrapper.appendTo($photographGallerySliderPhotographContainer);
        } else {
          $photographWrapper
            .empty()
            .append($photographGallerySliderPhotographLoadingSpinner)
            .css({
              width: photographWrapperSize.width,
              height: photographWrapperSize.height
            })
            .append($photograph);
        }

        $photographGallerySliderOpenPhotographInNewTabButton.attr("href", galleryItemData.gallerySliderImageFile.url);
      }
    }

    function showPhotograph(galleryItemId) {
      openPhotographGallerySlider();

      photographGallerySlider.currentGalleryItemIndex = _.findIndex(photographGallerySlider.gallery.galleryItems, function(x) {
        return x.id === galleryItemId;
      });

      openPhotograph(photographGallerySlider.currentGalleryItemIndex);
    }

    function initializeEventHandlers() {
      $window.resize(function() {
        var currentPhotographGallerySliderPhotographContainerWidth = $photographGallerySliderPhotographContainer.width();
        var currentPhotographGallerySliderPhotographContainerHeight = $photographGallerySliderPhotographContainer.height();

        if (currentPhotographGallerySliderPhotographContainerWidth !== photographGallerySliderPhotographContainerWidth || currentPhotographGallerySliderPhotographContainerHeight !== photographGallerySliderPhotographContainerHeight) {
          photographGallerySliderPhotographContainerWidth = currentPhotographGallerySliderPhotographContainerWidth;
          photographGallerySliderPhotographContainerHeight = currentPhotographGallerySliderPhotographContainerHeight;

          var $photographWrapper = $photographGallerySliderPhotographContainer.find(".photograph-wrapper");

          if ($photographWrapper.length !== 0) {
            var galleryItemData = photographGallerySlider.gallery.galleryItems[photographGallerySlider.currentGalleryItemIndex].itemData;
            var photographWrapperSize = calculatePhotographWrapperSize(galleryItemData.gallerySliderImageFile.width, galleryItemData.gallerySliderImageFile.height);

            $photographWrapper.css({
              width: photographWrapperSize.width,
              height: photographWrapperSize.height
            });
          }
        }
      });

      $photographGallerySliderPhotographContainer.on("click", function(e) {
        if (photographGallerySlider.settings.closePhotographGallerySliderOnPhotographContainerClick) {
          if (e.target !== this) {
            return;
          }

          closePhotographGallerySlider();
        }
      });

      $photographGallerySliderPhotographContainer.on("click", ".photograph", function() {
        openPhotograph(++photographGallerySlider.currentGalleryItemIndex);
      });

      $photographGallerySliderCloseSliderButton.on("click", function() {
        closePhotographGallerySlider();
      });

      $photographGallerySliderPreviousPhotographButton.on("click", function() {
        openPhotograph(--photographGallerySlider.currentGalleryItemIndex);
      });

      $photographGallerySliderNextPhotographButton.on("click", function() {
        openPhotograph(++photographGallerySlider.currentGalleryItemIndex);
      });
    }

    (function() {
      createPhotographGallerySlider();
      initializeEventHandlers();
    })();
  };

  $.fn.photographGallerySlider = function(options) {
    return this.each(function() {
      var $this = $(this);

      if (!$this.data("photographGallerySlider")) {
        $this.data("photographGallerySlider", new $.photographGallerySlider(this, options));
      }
    });
  };
})(jQuery, _, window);