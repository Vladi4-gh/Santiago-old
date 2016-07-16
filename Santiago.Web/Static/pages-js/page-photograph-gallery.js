(function($) {
  "use strict";

  var $photographGallery = $(".js-photograph-gallery");

  function createPhotographGallery(photographCategoryAlias) {
    $photographGallery.photographGallery({
      galleryItemHtmlTag: "article",
      getDataAction: {
        url: "/api/photographs/getphotographsbycategoryaliasorderedbycreationdatedesc"
      },
      categoryAlias: photographCategoryAlias,
      cellSize: {
        width: 200,
        height: 200
      },
      texts: {
        noPhotographsLoadedAndGalleryEmptyText: "<div class='photograph-gallery-message-box-block'>Фотографий с такой категорией нет =(</div><div class='photograph-gallery-message-box-block'>Попробуйте выбрать другую категорию в меню выше.</div>",
        noPhotographsLoadedButGalleryNotEmptyText: "<div class='photograph-gallery-message-box-block'>Хотите себе такие красивые фотографии? Тогда скорее звоните и закажите у меня фотосессию!</div>"
      }
    });
  }

  function getPhotographCategory() {
    var photographCategoryAlias = location.hash.replace("#", "");

    if (photographCategoryAlias.length === 0) {
      photographCategoryAlias = $(".js-photograph-categories-menu a:first").attr("href").replace("#", "");
    }

    return photographCategoryAlias;
  }

  function initializeEventHandlers() {
    $(".js-photograph-categories-menu a").on("click", function() {
      var $selectedPhotographCategoryMenuItemLink = $(this);

      if (!$selectedPhotographCategoryMenuItemLink.hasClass("active")) {
        $(".js-photograph-categories-menu a").removeClass("active");
        $selectedPhotographCategoryMenuItemLink.addClass("active");

        var photographCategoryAlias = $selectedPhotographCategoryMenuItemLink.attr("href").replace("#", "");

        $photographGallery.data("photographGallery").changePhotographCategory(photographCategoryAlias);
      }
    });
  }

  $(function() {
    var photographCategoryAlias = getPhotographCategory();

    $(".js-photograph-categories-menu a[href='#" + photographCategoryAlias + "']").addClass("active");

    createPhotographGallery(photographCategoryAlias);
    initializeEventHandlers();
  });
})(jQuery);