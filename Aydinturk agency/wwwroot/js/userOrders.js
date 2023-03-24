var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#UserOrdersTable').DataTable({
        "ajax": {
            "url": "/Customer/Orders/GetUserOrders"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "flightId", "width": "10%" },
            {
                "data": "orderDate", "width": "10%"
            
            },
            { "data": "orderStatus", "width": "10%" },
            { "data": "orderTotal", "width": "10%" },

            
            
            
        ]

    });
}


function Delete(url) {
    Swal.fire({
        title: 'هل انت متأكد ؟',
        text: "لن تستطيع الرجوع في حال تمت العملية",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'نعم, احذف!'
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message)
                    } else {
                        toastr.error(data.message)
                    }
                }

            })
        }
    })
}