$(function () {
    List();
    $('#btnsave').on('click', Save);
});


function List() {

    try {
        
        const dataString = {};
        AjaxFunc(dataString, "/UserMaster/List",).done(function (response) {
            let obj = response;
            if (obj.status == '200') {
                BindTable('tblDetail', obj.data.Table, true, true);
            }
            else {

            }

        }).fail(function (result) {
            console.log(result.Message);
        });

    } catch (e) {
        console.log(e.message);

    }
}

function Save() {

    try {
        if (!$('#Input_form').valid())
            return false;
        const dataString = {};
        dataString.Name = $('#Name').val();
        dataString.Mobile = $('#Mobile').val();
        
        AjaxFunc(dataString, "/UserMaster/SaveUpdate",).done(function (response) {
            let obj = response;
            if (obj.status == '200') {
                alert('success');
                $('.nav-tabs a[href="#tab1default"]').tab('show');
                List();
                ClearForm();
            }
            else {

            }

        }).fail(function (result) {
            console.log(result.Message);
        });

    } catch (e) {
        console.log(e.message);

    }
}

function Update() {

    try {
        if (!$('#Input_form').valid())
            return false;
        const dataString = {};
        dataString.Name = $('#Name').val();
        dataString.Mobile = $('#Mobile').val();
        dataString.id = id;

        AjaxFunc(dataString, "/UserMaster/SaveUpdate",).done(function (response) {
            let obj = response;
            if (obj.status == '200') {
                alert('success');
                List();
                $('.nav-tabs a[href="#tab1default"]').tab('show');
                
                $("#btnsave").off('click');
                $('#btnsave').on('click', Save);
                $('#btnsave').text('Save');
                ClearForm();
            }
            else {

            }

        }).fail(function (result) {
            console.log(result.Message);
        });

    } catch (e) {
        console.log(e.message);

    }
}


function Edit(e) {

    try {
        
        const dataString = {};
        dataString.id = e;
        AjaxFunc(dataString, "/UserMaster/Edit",).done(function (response) {
            let obj = response;
            if (obj.status == '200') {
                $('.nav-tabs a[href="#tab2default"]').tab('show');
                $('#Name').val(obj.data[0].Name);
                $('#Mobile').val(obj.data[0].Mobile);
                id = e;
                $('#btnsave').off('click');
                $('#btnsave').text('Update');
                $('#btnsave').on('click', Update);
            }
            else {

            }

        }).fail(function (result) {
            console.log(result.Message);
        });

    } catch (e) {
        console.log(e.message);

    }
}


function Delete(e) {

    try {

        const dataString = {};
        dataString.id = e;
        AjaxFunc(dataString, "/UserMaster/Delete",).done(function (response) {
            let obj = response;
            if (obj.status == '200') {
                List();
                ClearForm();
            }
            else {

            }

        }).fail(function (result) {
            console.log(result.Message);
        });

    } catch (e) {
        console.log(e.message);

    }
}