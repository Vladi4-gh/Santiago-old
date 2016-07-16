var editorExtensions = (function($) {
  "use strict";

  function onPasswordEditorVisibilityToggleButtonClick($visibilityToggleButton) {
    var $passwordInput = $visibilityToggleButton.siblings(".js-password-input");

    switch ($passwordInput.prop("type")) {
      case "password":
        $passwordInput.prop("type", "text");
        $visibilityToggleButton
          .prop("title", "Скрыть пароль")
          .html("<i class='fa fa-eye-slash fa-lg'></i>");

        break;
      case "text":
        $passwordInput.prop("type", "password");
        $visibilityToggleButton
          .prop("title", "Отобразить пароль")
          .html("<i class='fa fa-eye fa-lg'></i>");

        break;
    }
  }

  function initializeLogInPasswordEditorEventHandlers(logInForm) {
    logInForm.find(".js-editor .js-visibility-toggle-button").on("click", function() {
      onPasswordEditorVisibilityToggleButtonClick($(this));
    });
  }

  function initializePopupPasswordEditorEventHandlers(popupForm) {
    popupForm.find(".js-editor .js-visibility-toggle-button").on("click", function() {
      onPasswordEditorVisibilityToggleButtonClick($(this));
    });
  }

  return {
    initializeLogInPasswordEditorEventHandlers,
    initializePopupPasswordEditorEventHandlers
  };
})(jQuery);