var dataTable;


$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#OrdersTable').DataTable({
        "ajax": {
            "url": "/Admin/Orders/GetAll"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "flightId", "width": "15%" },
            {
                "data": "orderDate", "width": "15%"

            },
            { "data": "orderStatus", "width": "15%"},
            { "data": "orderTotal", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <div class="w-75 btn-group" role="group">
                             <a href="/Admin/Orders/Confirmation/${data}"
                                class="btn btn-info mx-2">
                                <i class="bi bi-pencil-square"></i> &nbsp; تفاصيل</a>
                            
                         </div>`;
                }
            },




        ]

    });
}