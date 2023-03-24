var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#flightsTable').DataTable({
        "ajax": {
            "url": "/Admin/flights/GetAll"
        },
        "columns": [
            { "data": "company.name", "width": "10%" },
            { "data": "from.name", "width": "10%" },
            { "data": "to.name", "width": "10%" },
            { "data": "date", "width": "10%" },
            { "data": "time", "width": "10%" },
            { "data": "sets", "width": "10%" },
            { "data": "weight", "width": "10%" },
            { "data": "price", "width": "10%" },
            
            
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <div class="w-75 btn-group" role="group">
                             <a href="/Admin/flights/edit/${data}"
                                class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> &nbsp; تعديل</a>
                             <a onClick=Delete('/Admin/flights/Delete/${data}')
                                class="btn btn-danger mx-2">
                                <i class="bi bi-trash"></i> &nbsp; حذف
                             </a>
                         </div>`;
                }
            },
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