/// <reference path="../uat/updateemployee.html" />
/// <reference path="../uat/updateemployee.html" />
///<reference path="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js">///
var Table = $('#tblentry', '.bs-example');
var url = '/api/test'
var Arr = [];
var SpReturnVal = '';
var employeedetails =
    {



        GetData: function () {
            $.ajax({
                url: '/api/test/getdata',
                dataType: 'json',
                async: false,
                cache:false,
                contentType: 'application/json',
                success: function (data) {
                    employeedetails.bindHtml(data);
                }
            });

        },

        bindHtml: function (data) {
            var tbody = $('#tbody');

            tbody.html('');



            for (var i = 0; i < data.length; i++) {
                var gender = '';
                gender = (data[i].gender == false) ? 'Female' : 'Male';
                var td = '';
                var tr = '';
                Arr[i] = new Array();
                Arr[i].push(data[i].id, data[i]);

                tr += '<tr>';
                td += '<td>' + data[i].id + '</td>'
                td += '<td>' + data[i].empName + '</td>'
                td += '<td>' + data[i].empLastName + '</td>'
                td += '<td>' + gender + '</td>'
                td += '<td>' + data[i].city + '</td>'
                td += '<td>' + data[i].designation + '</td>'
                td += '<td>' + data[i].salary + '</td>'
                td += '<td> <a style="cursor:pointer"  href="./UpdateEmployee.html?id=' + data[i].id + '"  >Update</a> <a style="cursor:pointer"  onclick="employeedetails.ConfirmDelete(' + data[i].id + ')" >Delete</a></td>'

                tr = tr + td
                tr += '</tr>'
                tbody.append(tr);
            }

            var localData = window.localStorage;
            var persistentData = window.localStorage;
            var jsonOrder = JSON.stringify(data);
            persistentData.setItem("myArr", jsonOrder);
        }

        , SendRequest: function (data, methodName, type) {
            var req = new XMLHttpRequest();
            req.onreadystatechange = function () {
                if (req.readyState == 4) {
                    var resp = req.responseText;
                    if (resp != "" && resp != null)
                        alert(resp);

                }
            }
            req.open(type, url + "/" + methodName, false);
            req.setRequestHeader('Content-Type', 'application/json');
            req.send(JSON.stringify(data));
        }
        , ConfirmDelete: function (id) {
            var q = confirm('Are your sure to Delete..??');
            if (q) {

                employeedetails.DeleteEmployee(parseInt(id));
                employeedetails.GetData();
            }
        }

        , InsertEmployee: function (employee) {
            var methodname = "ImportEmployee"
            methodname = "Insert"
            employeedetails.SendRequest(employee, methodname, 'POST');
        }


        , UpdateEmployee: function (employee) {
            var methodname = "UpdateEmployee"
            methodname = "Update"
           employeedetails.SendRequest(employee, methodname, 'PUT');
        },

        DeleteEmployee: function (empId) {
            var methodName = "Delete"
            $.support.cors = true;
            var url1 = url + "/" + methodName
            var data = {
                "id": empId
            };

            var req = new XMLHttpRequest();
            req.onreadystatechange = function () {
                if (req.readyState == 4) {

                    var resp = req.responseText;

                    if (resp != "" && resp != null)
                        alert(resp);

                }

            }
            req.open('DELETE', url1, false);
            req.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
            req.send(JSON.stringify(data));


        }
        , getParameterByName: function (name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }
    };
