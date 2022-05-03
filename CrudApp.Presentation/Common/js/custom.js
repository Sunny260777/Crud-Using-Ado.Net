var edit_uid = 0;


function AjaxFunc(dataString, url,) {
    try {
       
        var dfr = $.Deferred(),
            errorCount = 0;
        (function makeRequest() {
            $.ajax({
                url: url,
                type: 'POST',
                Accept: 'application/json',
                data: JSON.stringify(dataString),
                contentType: "application/json",
                dataType: "json",
                success: function (response) {
                    var result = response;
                    if (result.status == false) {
                        if (result.message == "logout") {
                            window.location.href = "/login/login";
                        }
                        else {
                            dfr.reject(result);
                        }
                    }
                    else {
                        dfr.resolve(result);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    dfr.reject(errorThrown);
                }
            });
        }());
        return dfr.promise();
    }
    catch (ex) {
        console.log(ex.message);
    }
}

function BindDropdown(data, Dropdown_Id, Value, text, ZeroIndex) {
    const DataCount = data.length;
    if (data != undefined && DataCount > 0) {
        var Data = '';
        if (ZeroIndex != '0')
            Data = '<option value="0">---' + ZeroIndex + '---</option>';
        for (var i = 0; i < data.length; i++) {
            Data += '<option value="' + data[i][Value] + '">' + data[i][text] + '</option>';
        }
        $('#' + Dropdown_Id).html(Data);
    }
    else {
        $('#' + Dropdown_Id).html('<option value="0">---' + ZeroIndex + '---</option>');
    }
}


function HandleNullTextValue(value) {
    return value == null ? '' : value;
}

function HandleNullDropdownValue(value) {
    return value == null ? '0' : value;
}

function BindTable(tblid, data, isdelete, isedit) {
    try {
        const totalcount = data.length;
        if (totalcount == 0) {
            $('#' + tblid).html('<tr style="text-align:center;"><td>No record Found</td></tr>');
            return false;
        }
        var html = '<thead><tr>';
        var datahead = Object.keys(data[0]);
        const totalhead = datahead.length;
        if (isdelete || isedit)
            html += '<th>Action</th>';
        for (var p = 0; p < totalhead; p++) {
            if (datahead[p] != 'id')
                html += '<th>' + datahead[p].replace(/_/g, ' ').replace(/per/g, '%') + '</th>';
        }

        html += '</thead><tbody>';
        for (var i = 0; i < totalcount; i++) {
            html += '<tr>';
            if (isdelete || isedit) {
                html += '<td>';
                if (isedit)
                    html += '<a class="btn btn-xs btn-outline-secondary" title="Edit" onclick="return Edit(`' + data[i].id + '`);">Edit</a>&nbsp;';
                if (isdelete)
                    html += '<a class="btn btn-xs btn-outline-secondary" title="delete" onclick="return Delete(`' + data[i].id + '`)">Delete</a>';
                html += '</td>';
            }
            for (var k = 0; k < totalhead; k++) {
                if (datahead[k] != 'id')
                    html += '<td>' + HandleNullTextValue(data[i][datahead[k]]) + '</td>';
            }

            html += '</tr>';
        }
        html += '</tbody>';
        $('#' + tblid).html(html);
        //ExportTableData(tblid);
    } catch (e) {
        console.log(e);
    }
}

function ClearForm() {
    $("#Input_form").trigger('reset');
    $('.field-validation-valid').html('');
    id = 0;
}

