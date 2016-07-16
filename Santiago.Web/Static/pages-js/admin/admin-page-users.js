(function($, _) {
  "use strict";

  var $usersDataTable = $(".js-users-data-table");
  var currentUserId;

  function createUsersDataTable() {
    $usersDataTable.dataTable({
      columns: [
        {
          field: "userName",
          title: "Имя"
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
          url: "/Admin/Users/GetUsersOrderedByCreationDateDesc"
        },
        createAction: {
          url: "/Admin/Users/CreateUser"
        },
        updateAction: {
          url: "/Admin/Users/UpdateUser"
        },
        deleteAction: {
          url: "/Admin/Users/DeleteUser"
        }
      },
      minWidth: 700,
      height: 369,
      popups: {
        addItemPopup: {
          title: "Добавление нового пользователя",
          content: $(".popups .js-add-user-popup").html(),
          onPopupOpen: function($popup) {
            editorExtensions.initializePopupPasswordEditorEventHandlers($popup.element.find("form"));
          }
        },
        editItemPopup: {
          title: "Редактирование пользователя",
          content: $(".popups .js-edit-user-popup").html()
        },
        deleteItemPopup: {
          title: "Удаление пользователя",
          content: $(".popups .js-delete-user-popup").html()
        }
      },
      texts: {
        addButtonTitle: "Добавить пользователя",
        addButtonText: "<i class='fa fa-plus fa-lg'></i><span class='command-button-text'>Добавить пользователя</span>",
        editButtonTitle: "Редактировать пользователя",
        deleteButtonTitle: "Удалить пользователя"
      },
      onContentLoad: onUsersDataTableContentLoad
    });
  }

  function onUsersDataTableContentLoad() {
    $usersDataTable.find("tbody tr").each(function() {
      var $tr = $(this);

      if ($tr.data("item-id") === currentUserId) {
        $tr.find(".command-button.delete-button")
          .prop("title", "Нельзя удалить текущего пользователя")
          .addClass("disabled")
          .prop("disabled", true);
      }
    });
  }

  function initializeEventHandlers() {
    $usersDataTable.on("click", ".popup form .js-change-user-password", function() {
      var editingUserId = $(this).closest(".popup").find("#Id").val();
      $("<div></div>").appendTo($usersDataTable).popup({
        title: "Смена пароля пользователя",
        content: $(".popups .js-change-user-password-popup").html(),
        onPopupOpen: function($popup) {
          var $changeUserPasswordPopupForm = $popup.element.find("form");

          $changeUserPasswordPopupForm.find("#Id").val(editingUserId);
          editorExtensions.initializePopupPasswordEditorEventHandlers($changeUserPasswordPopupForm);

          $.validator.unobtrusive.parse($changeUserPasswordPopupForm);

          $changeUserPasswordPopupForm.on("submit", function(e) {
            e.preventDefault();

            if ($(this).valid()) {
              $.ajax({
                type: "POST",
                url: "/Admin/Users/ChangeUserPassword",
                data: $(this).serialize(),
                success: function(serverValidationErrorsObject) {
                  if (serverValidationErrorsObject) {
                    $changeUserPasswordPopupForm.validate().showErrors(JSON.parse(serverValidationErrorsObject));
                  } else {
                    $popup.destroyPopup();
                  }
                },
                error: function() {
                  alert("Произошла неизвестная ошибка!");
                }
              });
            }
          });

          $changeUserPasswordPopupForm.on("click", ".close-button", function() {
            $popup.destroyPopup();
          });
        }
      });
    });
  }

  $(function() {
    createUsersDataTable();
    initializeEventHandlers();

    currentUserId = $(".js-current-user-id").val();
  });
})(jQuery, _);