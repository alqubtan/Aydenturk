var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#companiesTable').DataTable({
        "ajax": {
            "url": "/Admin/companies/GetAll"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "description", "width": "20%" },
            {
                "data": "imageUrl", "width": "20%", render: function (data) {

                    return `<img src="${data}" width=50 height=50 alt="" />`
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <div class="w-75 btn-group" role="group">
                             <a href="/Admin/companies/edit/${data}"
                                class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> &nbsp; تعديل</a>
                             <a onClick=Delete('/Admin/companies/Delete/${data}')
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