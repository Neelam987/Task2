
var userId = "";

function InsertSignInData() {
    debugger;
    var UserId = $('#txt_UserId').val();
    var Password = $('#txt_Password').val();
    if (confirm("Are You shure?")) {
        $.ajax({
            url: '/home/SubmitFormSignIn?UserId=' + UserId.toString() + '&Password=' + Password.toString(),
            type: 'get',
            dataType: 'json',
            contentType: 'Application/json charset=utf-8',
            success: function (data) {
                if (data.responseCode == 200) {
                    window.location.href = "/home/ThankYou";

                    //if (data.responseResult[0].Email == UserId && data.responseResult[0].Password == Password) {
                    //    window.location.href = "/home/ThankYou";
                    //   /* $("#txt_email").val(usedata.responseResult[0].UserId);*/
                    //}
                    //else {
                    //    alert("Invalid User Id And password")
                    //}
                    // $("#txt_email").val(usedata.responseResult[0].UserId);
                }
                else {
                    alert(data.responseMessage);
                }
            },
            error: function (xhr) {
                console.log(xhr);
            }
        });
    }
}
$("#txt_email").val(userId);



function InsertData() {
    debugger
    var Name = $('#txt_name').val();   
    var Email = $('#txt_email').val();
    var Gender = $('input[name="radiogender"]:checked').val();
    var MobileNumber = $('#txt_Mobilenumber').val();
    var Address = $('#txt_Address').val();
    var PinCode = $('#txt_PinCode').val();
    var Password = $('#txt_Password').val();
  

    var obj = new Object();
    obj.Name = Name.toString();
    obj.Email = Email.toString();
    obj.Gender = Gender.toString();
    obj.Address = Address.toString();
    obj.MobileNumber = MobileNumber.toString();
    obj.PinCode = PinCode.toString();
    obj.Password = Password.toString();

    if (confirm("Are You shure?")) {
        $.ajax({
            url: '/home/SubmitFormDetails',
            type: 'post',
            dataType: 'json',
            data: JSON.stringify(obj),
            contentType: 'Application/json charset=utf-8',
            success: function (data) {
                debugger
                alert("Data Save Successfully...")
                window.location.reload();               
            },
            error: function (xhr) {
                console.log(xhr);
            }
        });
    }

}


