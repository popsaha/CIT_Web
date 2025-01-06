
$(document).ready(function () {
    $.ajax(
        {
            type: 'POST',
            dataType: 'JSON',
            url: '/Report/GetAllReportData',
            //data: { Customer: $("#CustomerSelectedText").html(), Branch: $("#BranchSelectedText").html(), Service: $("#ServiceSelectedText").html() },
            success:
                function (response) {
                    createGridData(response);
                },
            error:
                function (response) {
                    console.log("Error:" + response)
                }
        });

    return false;
});

(function ($) {
    var CheckboxDropdown = function (el) {
        console.log("CheckboxDropdown Called")
        var _this = this;
        this.isOpen = false;
        this.areAllChecked = false;
        this.$el = $(el);
        console.log(this.$el);
        this.$label = this.$el.find('.dropdown-label');
        this.$checkAll = this.$el.find('[data-toggle="check-all"]').first();
        this.$inputs = this.$el.find('[type="checkbox"]');
        this.$CustomerSelectDropDown = this.$el.find('[name="Service-dropdown-group"]').val() == undefined ? "Branch" : "Service";
        // this.$BranchSelectDropDown = this.$el.find('[name="Branch-dropdown-group"]');

        this.onCheckBox();

        this.$label.on('click', function (e) {
            e.preventDefault();
            _this.toggleOpen();
        });

        this.$checkAll.on('click', function (e) {
            e.preventDefault();
            _this.onCheckAll();
        });

        this.$inputs.on('change', function (e) {
            _this.onCheckBox();
        });
    };

    CheckboxDropdown.prototype.onCheckBox = function () {
        this.updateStatus();
    };

    CheckboxDropdown.prototype.updateStatus = function () {
        var checked = this.$el.find(':checked');

        this.areAllChecked = false;
        this.$checkAll.html('Check All');

        if (checked.length <= 0) {
            this.$label.html('--Select ' + this.$CustomerSelectDropDown + '--');
        }
        else if (checked.length === 1) {
            this.$label.html(checked.parent('label').text());
            //console.log("NamedroCust" + this.$CustomerSelectDropDown);

        }
        else if (checked.length === this.$inputs.length) {
            this.$label.html('All Selected');
            this.areAllChecked = true;
            this.$checkAll.html('Uncheck All');
        }
        else {
            this.$label.html(checked.length + ' Selected');
            this.$label.html(checked.parent('label').text());
            //console.log("NamedroMore1Cust" + this.$CustomerSelectDropDown);

        }
    };

    CheckboxDropdown.prototype.onCheckAll = function (checkAll) {
        if (!this.areAllChecked || checkAll) {
            this.areAllChecked = true;
            this.$checkAll.html('Uncheck All');
            this.$inputs.prop('checked', true);
        }
        else {
            this.areAllChecked = false;
            this.$checkAll.html('Check All');
            this.$inputs.prop('checked', false);
        }

        this.updateStatus();
    };

    CheckboxDropdown.prototype.toggleOpen = function (forceOpen) {
        var _this = this;

        if (!this.isOpen || forceOpen) {
            this.isOpen = true;
            this.$el.addClass('on');
            $(document).on('click', function (e) {
                if (!$(e.target).closest('[data-control]').length) {
                    _this.toggleOpen();
                }
            });
        }
        else {
            this.isOpen = false;
            this.$el.removeClass('on');
            $(document).off('click');
        }
    };

    var checkboxesDropdowns = document.querySelectorAll('[data-control="checkbox-dropdown"]');
    for (var i = 0, length = checkboxesDropdowns.length; i < length; i++) {
        new CheckboxDropdown(checkboxesDropdowns[i]);
    }
})(jQuery);

function FeatchData() {
    var data = "";
    $("#Service input[type=checkbox]:checked").each(function () {
        //for Finding checked value
        data = data + $(this).val() + ",";

        //For Finding check text
        //data = data + $(this).parent('label').text() + ","; 
    });
    $("#ServiceSelectedText").html(data.substr(0, data.length - 1));

    data = "";
    $("#Branch input[type=checkbox]:checked").each(function () {
        //for Finding checked value
        data = data + $(this).val() + ",";

        //For Finding check text
        //data = data + $(this).parent('label').text() + ","; 
    });
    $("#BranchSelectedText").html(data.substr(0, data.length - 1));

    $('#tbody').empty();

    const requestData = {
        Customerid: $("#CustomerSelectedText").html(),
        Branchid: $("#BranchSelectedText").html(),
        PickupTypeid: $("#ServiceSelectedText").html(),
        fromDate: $("#fromdate").val(),
        ToDate: $("#todate").val(),
        SaveTaskData: ""
    };

    console.log("Post requestData : " + requestData);

    $.ajax(
        {
            type: 'POST',
            contentType: 'application/json',
            url: '/Report/GetFilterReports',
            data: JSON.stringify(requestData),
            success:
                function (response) {

                    createGridData(response);
                },
            error:
                function (response) {
                    console.log("Error:" + response)
                }
        });

    return false;
}

function createGridData(response) {
    var obj = JSON.stringify(response)
    console.log("Sucess:" + obj)
    //obj.each(myFunction)
    let dynamicRowHTML = "";
    if (response.length > 0) {
        dynamicRowHTML = '<table class="table table-hover border-primary border-1 table-bordered mt-3">' +
            '<thead class="bg-info">' +
            '<tr>' +
            '<th scope="col">Select</th>' +
            '<th scope="col">CustomerName</th>' +
            '<th scope="col">BranchName</th>' +
            '<th scope="col">PickupType</th>' +
            '<th scope="col">Trip</th>' +
            '<th scope="col">Bill</th>' +
            '<th scope="col">Discount</th>' +
            '<th scope="col">Final Bill</th>' +
            '</tr>' +
            '</thead>' +
            '<tbody>';


        $.each(JSON.parse(obj), function (Key, Value) {

            dynamicRowHTML = dynamicRowHTML + '<tr>' +
                '<td> <input type="checkbox" id="' + (Key + 1) + '"/></td>' +
                '<td style="display:none" id="TaskID' + (Key + 1) + '">' + Value.taskID + '</td>' +
                '<td>' + Value.customerName + '</td>' +
                '<td>' + Value.branchName + '</td>' +
                '<td>' + Value.pickupTypeName + '</td>' +
                '<td>' + Value.trip + '</td>' +
                '<td> <span id="Bill' + (Key + 1) + '">' + Value.bill + '</span></td>' +
                '<td><select id="discount' + (Key + 1) + '" onchange="GetDiscount(' + (Key + 1) + ')"><option>0</option>' +
                '<option>1</option>' +
                '<option>2</option>' +
                '<option>3</option>' +
                '<option>4</option>' +
                '<option>5</option></select></td>' +
                '<td id="finalCalc' + (Key + 1) + '">' + Value.bill + '</td>' +
                '</tr>';

        });
        dynamicRowHTML = dynamicRowHTML + '</tbody></table>';
        $('#tbody').append(dynamicRowHTML);
    }
    else {

        $('#tbody').append("<div class='no-data-message'>Oops No data Found</div>");
    }
}

function myFunction(item, index) {
    text += index + ": " + item + "<br>";
}

// It will print the selected value 
function displayNum() {
    $("#CustomerSelectedText").html($("select#IdCustomerDrop").val());
}
$("select#IdCustomerDrop").change(displayNum);

function GetDiscount(id) {
    console.log("GetDiscounCalled");
    $("#finalCalc" + id).text($("#Bill" + id).text() - (($("#Bill" + id).text() * $("#discount" + id).val()) / 100) + ".00");
}

function BtnGenratebill() {

    $("#BillSubmitProcess").text("");
    var CheckedID = [];

    $("input:checkbox").each(function () {
        var $this = $(this);
        if ($this.is(":checked")) {
            CheckedID.push($this.attr("id"));
        }
    });
    if (CheckedID.length == 0)
        $("#BtnSave").hide();
    else
        $("#BtnSave").show();
    //console.log("CheckedID check", CheckedID.length);

    var total = 0; $.each(CheckedID, function (index, value) {
        total = total + parseInt($("#finalCalc" + value).text())
    });

    $("#TotalAmount").text(total);
    var tax = (total * 5) / 100;
    var vat = (total * 10) / 100
    $("#Tax").text(tax);
    $("#Vat").text(vat);
    $("#FinalAmount").text(total + vat + tax);
}


function BtnSaveBill() {

    $("#BtnSave").hide(); //button disable
    $("#BillSubmitProcess").text("Details Saving Please Wait....");
    $('#BillSubmitProcess').addClass("SucessText");

    var CheckedID = [];
    $("input:checkbox").each(function () {
        var $this = $(this);
        if ($this.is(":checked")) {
            CheckedID.push($this.attr("id"));
        }
    });
    var ReqData = "";
    $.each(CheckedID, function (index, value) {
        var AfterDsicount = $("#finalCalc" + value).text();
        var tax = (AfterDsicount * 5) / 100;
        var vat = (AfterDsicount * 10) / 100;
        var FinalAmount = parseInt(AfterDsicount) + parseInt(vat) + parseInt(tax);
        ReqData = ReqData + $("#TaskID" + value).text() + "|" + $("#Bill" + value).text() + "|" + $("#discount" + value).val() + '|' + AfterDsicount + "|" + tax + "|" + vat + "|" + FinalAmount + ",";

    });
    ReqData = ReqData.slice(0, -1);
    //console.log("SaveBill " + ReqData);

    const requestData = {
        Customerid: $("#CustomerSelectedText").html(),
        Branchid: $("#BranchSelectedText").html(),
        PickupTypeid: $("#ServiceSelectedText").html(),
        fromDate: $("#fromdate").val(),
        ToDate: $("#todate").val(),
        SaveTaskData: ReqData
    };

    $.ajax(
        {
            type: 'POST',
            contentType: 'application/json',
            url: '/Report/SaveReportsData',
            data: JSON.stringify(requestData),
            success:
                function (response) {
                    $("#BillSubmitProcess").text("Details Save Sucessfully");
                    $("#tbody").empty();
                    createGridData(response);
                },
            error:
                function (response) {
                    $("#BillSubmitProcess").text("Oops! Details Save Failed. Please try after sometime.");
                    $('#BillSubmitProcess').removeClass("SucessText");
                    $('#BillSubmitProcess').addClass("failText");
                    $("#BtnSave").show();
                    console.log("Error:" + response)
                }
        });

    return false;
}
