$(document).ready(function () {
    $('#Categories').DataTable({
        "ajax": {
            url: "/categories/getall",
            datatype: "json",
            dataSrc: "",
        },
        "columns": [
            //{
            //    "name": "rownum",
            //    orderable: false,
            //    autoWidth: true,
            //    render: function (data, type, row, meta) {
            //        return meta.row + 1
            //    }
            //},
            {
                data: "id"
            },
            {
                data: "name",
                autowidth: true
            }

        ],
    });
});


function AddCategory() {
    event.preventDefault();
    var cry = {
        Name: $("#cryName").val()
    };
    $.ajax({
        url: "/categories/post",
        type: "POST",
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(cry)
    }).done((status) => {
        console.log(status);
        switch (status) {
            case 200:
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `Category ${cry.Name} has been added`,
                    showConfirmButton: false,
                    timer: 1500
                }).then(function () { location.reload() });
                break;
            default:
                //might add custom validation specifically to the form fields
                //var message = results.message;
                //if (message.toLowerCase().contains("phone")) {
                //    $('#phonefdbck').html(message);
                //    <script>?
                //}
                //...etc
                Swal.fire({
                    icon: 'error',
                    title: `Failed to add the category of ${cry.Name}`,
                    text: `Failed ${status}`,
                }).then(function () { });
        }
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Category addition failed',
            text: "Something went wrong",
            footer: '<a href="">Check the backend programming</a>'
        }).then(function () { });
    })
}