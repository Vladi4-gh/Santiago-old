(function($, _) {
  // Активирует валидацию hidden-полей.
  $.validator.setDefaults({
    ignore: []
  });

  $.validator.addMethod("notequal", function(value, element, params) {
    return value !== $(params).val();
  });

  $.validator.unobtrusive.adapters.add("notequal", ["propertyforcomparison"], function(options) {
    options.rules["notequal"] = "#" + options.params["propertyforcomparison"];
    options.messages["notequal"] = options.message;
  });
})(jQuery, _);