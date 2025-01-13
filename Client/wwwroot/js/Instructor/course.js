$(document).ready(function () {
    tableCourse;
    console.log(userId);
});

/* Table Course */
var tableCourse = $('#tableCourse').DataTable({
    "processing": true,
    "ajax": {
        "url": "/Courses/getbyuser/"+userId,
        "dataSrc": ""
    },
    "columns": [
        {
            "data": null,
            "render": function (data, type, row, meta) {
                return meta.row + meta.settings._iDisplayStart + 1;
            }
        },
        { "data": "id" },
        { "data": "title" },
        { "data": "description" },
        { "data": "features" },
        { "data": "price" },
        {
            "data": null,
            "render": function (data, type, row, meta) {
                switch (row["status"]) {
                    case 0:
                        return `<span class="badge badge-pill badge-warning">Incomplete</span>`
                        break;
                    case 1:
                        return `<span class="badge badge-pill badge-primary">Review</span>`
                        break;
                    case 2:
                        return `<span class="badge badge-pill badge-danger">Revise</span>`
                        break;
                    case 3:
                        return `<span class="badge badge-pill badge-success">Approve</span>`
                        break;
                }
            }
        },
        {
            "data": null,
            "render": function (data, type, row, meta) {
                return `<a href="/Instructor/Course/view/${row["id"]}" class="btn btn-sm btn-info" role="button"><i class="fas fa-arrow-right"></i></a>`
            }
        }
    ]
});
$("#submitButton").click(function () {
    var form = $(".needs-validation")
    event.preventDefault();
    if (form[0].checkValidity() === false) {
        event.stopPropagation();
    } else {
        Insert();
    }
    form.addClass('was-validated');
})
/*function Insert() {
    var obj = new Object();
    obj.Title = $("#title").val();
    obj.Description = $("#description").val();
    obj.Features = $("#features").val();
    obj.Price = parseInt($("#price").val());
    obj.UserId = "2022001";
    console.log(obj)
    $.ajax({
        url: "/Courses/Post/",
        type: "POST",
        contentType: 'application/json',
        data: JSON.stringify(obj),
        beforeSend: function () {
            swal.fire({
                title: 'Menunggu',
                html: 'Memproses data',
                didOpen: () => {
                    swal.showLoading()
                }
            })
        }
    }).done((result) => {
        tableCourse.ajax.reload();
        $(".needs-validation").removeClass('was-validated');
        $('#formInput').find('form').trigger('reset');
        $('#formInput').modal('hide');
        console.log(result)
        Swal.fire(
            'Berhasil',
            'Harap Untuk Menambahkan Content',
            'success'
        )
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal',
            error.responseText,
            'error'
        )
    })
}*/
function Insert() {
    var formData = new FormData();
    formData.append('foto', $('#foto').get(0).files[0]);
    formData.append('title', $("#title").val());
    formData.append('description', $("#description").val());
    formData.append('features', $("#features").val());
    formData.append('price', parseInt($("#price").val()));
    formData.append('userId', userId);
    console.log(formData)
    $.ajax({
        url: "/Courses/Upload/",
        type: "POST",
        processData: false,
        contentType: false,
        cache: false,
        data: formData,
        enctype: 'multipart/form-data',
        beforeSend: function () {
            swal.fire({
                title: 'Menunggu',
                html: 'Memproses data',
                didOpen: () => {
                    swal.showLoading()
                }
            })
        }
    }).done((result) => {
        tableCourse.ajax.reload();
        $(".needs-validation").removeClass('was-validated');
        $('#formInput').find('form').trigger('reset');
        $('#formInput').modal('hide');
        console.log(result)
        Swal.fire(
            'Berhasil',
            'Harap Untuk Menambahkan Content',
            'success'
        )
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal',
            error.responseText,
            'error'
        )
    })
}