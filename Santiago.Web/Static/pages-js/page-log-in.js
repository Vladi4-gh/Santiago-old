(function($) {
  "use strict";

  function initializeEventHandlers() {
    var $logInForm = $(".js-log-in-form");

    editorExtensions.initializeLogInPasswordEditorEventHandlers($logInForm);

    $logInForm.on("submit", function() {
      if ($(this).valid()) {
        $(".js-validation-error-message").hide();
        $(".js-log-in-spinner-container").show();
        $(".js-log-in-button")
          .addClass("disabled")
          .prop("disabled", true);
      }
    });
  }

  $(function() {
    initializeEventHandlers();
  });
})(jQuery);