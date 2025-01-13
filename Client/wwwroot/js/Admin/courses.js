/* table data read and population */
$(document).ready(function () {
    $('#Courses').DataTable({
        "ajax": {
            url: "/courses/getall",
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
                data: "title",
                autowidth: true
            },
            {
                data: null,
                orderable: false,
                render: function (data, type, row, meta) {
                    let status = data["status"];
                    var buttonState = "";
                    switch (status) {
                        case 0:
                            buttonState = "secondary";
                            status = "Incomplete";
                            break;
                        case 1:
                            buttonState = "primary";
                            status = "Review";
                            break;
                        case 2:
                            buttonState = "warning";
                            status = "Revise";
                            break;
                        case 3:
                            buttonState = "success";
                            status = "Approved";
                            break;
                    }
                    return `<div class="ml-auto mr-auto btn-group" role="group" aria-label="Basic example">
                                <button class="btn btn-${buttonState}">${status}</button> 
                                <button class="btn btn-light" onclick="CoursePage('${row.id}')"><i class="fa-solid fa-arrow-right-from-bracket"></i></button>
                            </div>`;
                },
            },
        ],
    });
});

function CoursePage(ID) {
    sessionStorage.setItem("CourseId", ID);
    location.assign('/admin/CourseReview');
}