/* table data read and population */
$(document).ready(function () {
    $('#TabelMasterData').DataTable({
        "filter": true,
        "orderMulti": false,
        "ajax": {
            url: "/users/getmasterdata",
            datatype: "json",
            dataSrc: "result"  
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
                "data": "id"
            },
            {
                "data": "fullName"
            },
            {
                "data": "gender"
            },
            {
                "name": "birthdate",
                "data": null,
                "render": function (data, type, row) {
                    return moment(row["birthDate"]).format('LL');
                }
            },
            {
                "name": "phone",
                "data": null,
                "render": function (data, type, row) {
                    let phone = data["phoneNumber"];
                    if (phone != null && phone != '') {
                        if (phone[0] == '0') {
                            return "+62" + phone.substring(1);
                        }
                    }
                    return phone;
                },
                "autoWidth": true
            },
            {
                "data": "email"
            },
            {
                "data": "roles"
            },
            {
                "name": "opsi",
                "orderable": false,
                render: function (data, type, row, meta) {
                    return `<div class="row">
                                <div class="col-6">
                                <button class="btn btn-warning fa-solid fa-pen-to-square" onClick="GetUpdateModal('${row.id}')" data-toggle="modal" data-hover="tooltip"  data-placement="right" title="Edit User" data-target="#userEditModal"></button>
                                </div>
                                <div class="col-6">
                                <button class="btn btn-danger fa-solid fa-trash-can" onClick="Delete('${row.id}')" data-hover="tooltip" data-placement="right" title="Delete"></button>
                                </div>
                            </div>`;
                },
                "autowidth": true
            },
        ],
    });
});

$(document).ready(function () {
    $('#StudentTable').DataTable({
        "filter": true,
        "orderMulti": false,
        "ajax": {
            url: "/users/getstudents",
            datatype: "json",
            dataSrc: "result"
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
                "data": "id"
            },
            {
                "name": "fullName",
                "data": null,
                "render": function (data, type, row) {
                    return data["firstName"] + data["lastName"]
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (data["gender"] == 0) {
                        return "Laki-laki"
                    }
                    else {
                        return "Perempuan"
                    }
                }
            },
            {
                "name": "birthdate",
                "data": null,
                "render": function (data, type, row) {
                    return moment(row["birthDate"]).format('LL');
                }
            },
            {
                "name": "phone",
                "data": null,
                "render": function (data, type, row) {
                    let phone = data["phoneNumber"];
                    if (phone != null && phone != '') {
                        if (phone[0] == '0') {
                            return "+62" + phone.substring(1);
                        }
                    }
                    return phone;
                },
                "autoWidth": true
            },
            {
                "data": "email"
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
                            status = "Unrequested";
                            break;
                        case 1:
                            buttonState = "primary";
                            status = "Requested";
                            return `<div class="row ml-auto mr-auto">
                                    <button class="btn btn-${buttonState}" onclick="statusButton('${row.id}')">${status}</button>
                                    </div>`;
                        case 2:
                            buttonState = "success";
                            status = "Approved";
                            break;
                    }
                    return `<p>${status}</p>`
                },
                autowidth: true
            }
        ],
    });
});

function statusButton(ID) {
    Swal.fire({
        title: `Do you want to approve this student to be an instructor?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((answer) => {
        if (answer.isConfirmed) {
            var aty = {
                AccountId: ID,
                RoleId: 2
            };

            $.ajax({
                url: `/Authorities/postinstructorrole`,
                type: "POST",
                data: aty,
                dataType: "JSON"
            }).done((statusCode) => {
                if (statusCode == 200) {
                    var student = {
                        Id: ID,
                        Status: 1
                    };
                    $.ajax({
                        url: "/Users/InstructorSignup",
                        type: "PUT",
                        dataType: "JSON",
                        data: student
                    })
                    Swal.fire({
                        title: `Approved`,
                        text: `The student with ID ${ID} has become an instructor`,
                        type: "success"
                    }).then(function () {
                        location.reload();
                    }
                    );
                }
                else {
                    console.log(statusCode);
                    Swal.fire({
                        title: "Approve failed", text: results.message,
                        type: "failed"
                    }).then(function () { location.reload() });
                }
            }).fail((error) => {
                Swal.fire({
                    title: "Failed", text: "Something went wrong!",
                    type: "failed"
                }).then(function () { location.reload() });
            });
            
        }
    });
}

/* insert,update,delete */
function AddNewAdmin() {
    var register = {
        FirstName : $("#firstName").val(),
        LastName : $("#lastName").val(),
        BirthDate: $("#birthDate").val(),
        Gender : $("#gender").val(),
        PhoneNumber : $("#phone").val(),
        Email : $("#email").val(),
        Password: $("#password").val(),
        Role: 1
    };
    $.ajax({
        url: "/accounts/register",
        type: "POST",
        dataType: 'json',
        data: register
    }).done((results) => {
        switch (results.status) {
            case 200:
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'The new admin has been registered',
                    showConfirmButton: false,
                    timer: 1500
                }).then(function(){ location.reload() });
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
                    title: 'Registration failed',
                    text: results.message,
                }).then(function () { });        
        }
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Registration failed',
            text: "Invalid data",
            footer: '<a href="">Check the backend programming</a>'
        }).then(function () { });
    })
}

function GetUpdateModal(ID) {
    $.ajax({
        url: `/users/get/${ID}`,
        success: function (result) {
            console.log(result);
            $('#editID').val(`${result.id}`);
            $('#editFName').val(`${result.firstName}`);
            $('#editLName').val(`${result.lastName}`);
            $('#editEmail').val(`${result.email}`);
            $('#editPhone').val(`${result.phoneNumber}`);
            $('#editBDate').val(`${result.birthDate}`.toString().substring(0, 10));
            if (result.gender == 0) {
                $('#editGender').val("0")
            }
            else {
                $('#editGender').val("1")
            };
        }
    });
}

function Update() {
    event.preventDefault();
    var usr = {
        id: $("#editID").val(),
        firstName: $("#editFName").val(),
        lastName: $("#editLName").val(),
        phoneNumber: $("#editPhone").val(),
        email: $("#editEmail").val(),
        gender: $("#editGender").val(),
        birthDate: $("#editBDate").val()
    };
    $.ajax({
        url: `/users/put`,
        type: "PUT",
        dataType: 'JSON',
        data: usr
    }).done((statusCode) => {
        console.log(statusCode);
        switch (statusCode) {
            case 200:
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `The new data of ${usr.id} has been saved`,
                    showConfirmButton: false,
                    timer: 2500
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
                    title: 'Registration failed',
                    text: results.message,
                    footer: '<a href="">Check the backend programming</a>'
                }).then(function () { });
        }
    }).fail((error) => {
        Swal.fire({
            position: 'center',
            icon: 'failed',
            title: `Edit Failed`,
            showConfirmButton: false,
            timer: 1500
        })
    });
}

function Delete(ID) {
    Swal.fire({
        title: `Do you want to delete ${ID}?`,
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/users/delete/${ID}`,
                type: "DELETE"
            }).done((statusCode) => {
                if (statusCode == 200) {
                    Swal.fire({
                        title: "Delete Successful", text: `The employee data with NIK ${ID} has been deleted`,
                        type: "success"
                    }).then(function () {
                        location.reload();
                    }
                    );
                }
                else {
                    Swal.fire({
                        title: "Delete Failed", text: results.message,
                        type: "failed"
                    }).then(function() { location.reload() });
                }
            }).fail((error) => {
                Swal.fire({
                    title: "Delete Failed", text: "Something went wrong!",
                    type: "failed"
                }).then(function() { location.reload() });
            });
        }
    })
}

