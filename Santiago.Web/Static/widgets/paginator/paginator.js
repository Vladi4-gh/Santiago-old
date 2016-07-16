(function($) {
  "use strict";

  $.paginator = function(element, options) {
    var defaults = {
      additionalCssClass: "",
      currentPageNumber: 1,
      maxPageNumber: 1,
      textTemplate: "Страница {{= it.currentPageNumber }} из {{= it.maxPageNumber }}",
      texts: {
        firstButtonTitle: "В начало",
        previousButtonTitle: "Предыдущая страница",
        inputTitle: "Кликните, чтобы ввести номер страницы",
        nextButtonTitle: "Следующая страница",
        lastButtonTitle: "В конец"
      },
      onPageNumberChanged: function() { }
    };

    var paginator = this;

    paginator.setCurrentPageNumber = setCurrentPageNumber;
    paginator.setMaxPageNumber = setMaxPageNumber;
    paginator.disablePaginator = disablePaginator;
    paginator.enablePaginator = enablePaginator;

    function createPaginator() {
      paginator.settings = $.extend(true, defaults, options);
      paginator.element = $(element);

      paginator.element
        .addClass("paginator " + paginator.settings.additionalCssClass)
        .append(
          "<button type='button' class='left-button' data-action='first' title='" + paginator.settings.texts.firstButtonTitle + "'><i class='fa fa-angle-double-left fa-lg'></i></button>" +
          "<button type='button' class='left-button' data-action='previous' title='" + paginator.settings.texts.previousButtonTitle + "'><i class='fa fa-angle-left fa-lg'></i></button>" +
          "<input type='text' title='" + paginator.settings.texts.inputTitle + "' />" +
          "<button type='button' class='right-button' data-action='next' title='" + paginator.settings.texts.nextButtonTitle + "'><i class='fa fa-angle-right fa-lg'></i></button>" +
          "<button type='button' class='right-button' data-action='last' title='" + paginator.settings.texts.lastButtonTitle + "'><i class='fa fa-angle-double-right fa-lg'></i></button>");

      paginator.input = paginator.element.find("input");
      paginator.leftButtons = paginator.element.find(".left-button");
      paginator.rightButtons = paginator.element.find(".right-button");
    }

    function setInputText() {
      var templateFunction = doT.template(paginator.settings.textTemplate);

      paginator.input.val(templateFunction({
        currentPageNumber: paginator.settings.currentPageNumber,
        maxPageNumber: paginator.settings.maxPageNumber
      }));
    }

    function setButtonsState() {
      if (paginator.settings.currentPageNumber === 1 && paginator.settings.maxPageNumber === 1) {
        paginator.leftButtons
          .addClass("disabled")
          .prop("disabled", true);

        paginator.rightButtons
          .addClass("disabled")
          .prop("disabled", true);
      } else if (paginator.settings.currentPageNumber === 1 && paginator.settings.maxPageNumber > paginator.settings.currentPageNumber) {
        paginator.leftButtons
          .addClass("disabled")
          .prop("disabled", true);

        paginator.rightButtons
          .removeClass("disabled")
          .prop("disabled", false);
      } else if (paginator.settings.currentPageNumber > 1 && paginator.settings.maxPageNumber > paginator.settings.currentPageNumber) {
        paginator.leftButtons
          .removeClass("disabled")
          .prop("disabled", false);

        paginator.rightButtons
          .removeClass("disabled")
          .prop("disabled", false);
      } else if (paginator.settings.currentPageNumber === paginator.settings.maxPageNumber) {
        paginator.leftButtons
          .removeClass("disabled")
          .prop("disabled", false);

        paginator.rightButtons
          .addClass("disabled")
          .prop("disabled", true);
      }
    }

    function setCurrentPageNumber(pageNumber) {
      if (!isNaN(pageNumber) && pageNumber >= 1 && pageNumber <= paginator.settings.maxPageNumber) {
        paginator.settings.currentPageNumber = pageNumber;
        setButtonsState();
        paginator.settings.onPageNumberChanged(paginator.settings.currentPageNumber);
      }

      setInputText();
    }

    function setMaxPageNumber(pageNumber) {
      if (isNaN(pageNumber) || pageNumber < 1) {
        return;
      }

      paginator.settings.maxPageNumber = pageNumber;

      if (paginator.settings.currentPageNumber > pageNumber) {
        paginator.settings.currentPageNumber = pageNumber;
        setButtonsState();
        paginator.settings.onPageNumberChanged(paginator.settings.currentPageNumber);
      }

      setInputText();
    }

    function disablePaginator() {
      paginator.element.find("button")
        .addClass("disabled")
        .prop("disabled", true);

      paginator.element.find("input")
        .addClass("disabled")
        .prop("disabled", true);
    }

    function enablePaginator() {
      paginator.element.find("input")
        .removeClass("disabled")
        .prop("disabled", false);

      setButtonsState();
    }

    function initializeEventHandlers() {
      paginator.element.on("click", "button", function(e) {
        e.preventDefault();

        switch ($(this).data("action")) {
          case "first":
            if (paginator.settings.currentPageNumber === 1) {
              return;
            }

            paginator.settings.currentPageNumber = 1;

            break;
          case "previous":
            if (paginator.settings.currentPageNumber === 1) {
              return;
            }

            paginator.settings.currentPageNumber -= 1;

            break;
          case "next":
            if (paginator.settings.currentPageNumber === paginator.settings.maxPageNumber) {
              return;
            }

            paginator.settings.currentPageNumber += 1;

            break;
          case "last":
            if (paginator.settings.currentPageNumber === paginator.settings.maxPageNumber) {
              return;
            }

            paginator.settings.currentPageNumber = paginator.settings.maxPageNumber;

            break;
        }

        setButtonsState();
        setInputText();
        paginator.settings.onPageNumberChanged(paginator.settings.currentPageNumber);
      });

      paginator.input.on("focus", function() {
        paginator.input.val(paginator.settings.currentPageNumber);
        paginator.input.select().mouseup(function(e) {
          e.preventDefault();
        });
      });

      paginator.input.on("blur", function() {
        var currentPageNumber = parseInt($(this).val(), 10);

        if (currentPageNumber !== paginator.settings.currentPageNumber) {
          setCurrentPageNumber(currentPageNumber);
        } else {
          setInputText();
        }
      });

      paginator.input.on("keydown", function(e) {
        if (e.keyCode === 13 || e.keyCode === 27) {
          paginator.input.blur();
        }
      });
    }

    (function() {
      createPaginator();
      initializeEventHandlers();

      setButtonsState();
      setInputText();
    })();
  };

  $.fn.paginator = function(options) {
    return this.each(function() {
      var $this = $(this);

      if (!$this.data("paginator")) {
        $this.data("paginator", new $.paginator(this, options));
      }
    });
  };
})(jQuery);