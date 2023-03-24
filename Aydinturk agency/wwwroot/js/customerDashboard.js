$(document).ready(function () {

    getNotifications();
});

function getNotifications() {
    var unreadNotifications = 0;

    $.get("/Customer/Notifications/GetAll", function (data) {
        // Update the notification list with the latest data
        //console.log(data.value.data)
        $("#notificationList").empty();
        data.value.data.forEach(function (notification) {
            if (notification.seen == false) {
                unreadNotifications++;
            }
            //console.log(notification)
            $("#notificationList").append(
                '<a class="dropdown-item">' + notification.message + '</a>'
            );
        });
        // Set the number of notifications
        $("#notificationCount").text(unreadNotifications.toString());

    });



}

// Open Dropdown
$("#navbarDropdown").click(function () {
    $("#navbarDropdown").dropdown("toggle");
});

// Close Dropdown
$(document).click(function (event) {
    if (!$(event.target).closest(".dropdown").length) {
        $("#navbarDropdown").dropdown("toggle");
    }
});