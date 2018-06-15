

function onSignIn(googleUser) {

    loggedIn = 1;

    $('#gSignIn').hide();
    $("#emailDiv").hide();
    var profile = googleUser.getBasicProfile();

    //console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
    //console.log('Name: ' + profile.getName());
    //console.log('Image URL: ' + profile.getImageUrl());
    //console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.
    //console.log('token: ' + googleUser.getAuthResponse().id_token);
    $('#addmorebutton_link').show();
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var dateTime = date + ' ' + time;
    $('#uname').html('Welcome ' + profile.getName());
    mail_id = profile.getEmail();
    document.getElementById("foruseremail").value = profile.getEmail();
    document.getElementById("forusertime").value = dateTime;
    var found = false;
    var admin = true;

    for (var i = 0; i < emails.length; i++) {
        if (emails[i].Email == profile.getEmail()) {
            
            found = true;
            if (emails[i].Role == "user") {
                admin = false;
            }
            else if (emails[i].Role == "admin") {
                admin = true;
                break;

            }
        }
    }

    if (found) {

             if (admin) {
            $('#uemail').html('Admin Login: ' + profile.getEmail());
            $('#ufullname').html(profile.getName());

            $('#AddAdmins').show();

            $('#AddAdminsResp').show();
            $('#ViewRequests').show();
            $('#importDataPop').show();
         //   $('#ReportProblems').show();
            $('#ViewRequestsResp').show();
            $('#ViewProblems').show();
            $('#editlink').show();
            $('#deletelink').show();

            $('#unapprovelink').show();
            
            adminLoggedIn = 1;
        }
        else {
                 $('#AddAdmins').hide();
                 $('#AddAdminsResp').hide();
                 $('#importDataPop').hide();
                 $('#ViewRequests').hide();
                 $('#ViewProblems').hide();
           //      $('#ReportProblems').hide();
                 $('#ViewRequestsResp').hide();

            $('#ufullname').html(profile.getName());

            $('#uemail').html('User Login: ' + profile.getEmail());
            $('#editlink').hide();
            $('#deletelink').hide();
            $('#unapprovelink').hide();

            adminLoggedIn = 0;
        }

    }
        
    else {
        $('#editlink').hide();
        $('#deletelink').hide();
        adminLoggedIn = 0;

        if (profile.getEmail() != null) addUserEmail("user", profile.getEmail(), profile.getName());
        $('#uemail').html('User Login: ' + profile.getEmail());

    }
    gapi.client.load('plus', 'v1', function () {
        var request = gapi.client.plus.people.get({
            'userId': 'me'
        });
   
        request.execute(function (resp) {
            var profileHTML = 'Welcome ' + resp.name.givenName + '! <a href="javascript:void(0);" onclick="signOut();">Sign out</a>';
            $('.userContent').html('<a href="javascript:void(0);" onclick="signOut();"><b>Sign out</b></a>');

        });
        //Display the user details
        try{
            $('.modal_first').show();
        }
        catch(e){
            alert("please reload and retry!");
        }
    });

}

function signOut() {
    adminLoggedIn = 0;
    var auth2 = gapi.auth2.getAuthInstance();
    $("#emailDiv").show();

    auth2.signOut().then(function () {
        $('#uemail').html('You are not logged in!');
        $('#addmorebutton_link').hide();
        $('#editlink').hide();
        $('#deletelink').hide();
        $('#unapprovelink').hide();

        $('#gSignIn').show();
        $('.userContent').html("");
        $('#ufullname').html("");
    });
   // window.location.href = "http://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://servirglobal.net/MapResources/LULC_Africa/Home.aspx";
    window.location.href = "http://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue="+ window.location.protocol + "//" + window.location.host + "/Home.aspx";
}

function ViewDataFromRequests(_uid) {
    for (var x = 0; x < completedArrayTemp.length; x++) {
        if (completedArrayTemp[x].UID == _uid) {
            document.getElementById("spanfora").innerHTML = decodeURIComponent(completedArrayTemp[x].Title);
            document.getElementById("spanfora0").innerHTML = "View Data for " + decodeURIComponent(completedArrayTemp[x].Title);
            document.getElementById("spanforb").innerHTML = completedArrayTemp[x].CategoryName;
            document.getElementById("spanforc").innerHTML = completedArrayTemp[x].MapYear;
            document.getElementById("spanford").innerHTML = completedArrayTemp[x].Organization;
            if (completedArrayTemp[x].NumberOfClasses == 0) {
                document.getElementById("spanfore").innerHTML = "Not specified";
            }
            else {
                document.getElementById("spanfore").innerHTML = completedArrayTemp[x].NumberOfClasses;
            }
            document.getElementById("spanforf").innerHTML = completedArrayTemp[x].DataSource;
            document.getElementById("spanforg").innerHTML = completedArrayTemp[x].Status;
            if (completedArrayTemp[x].ReleasedYear == 0) {
                document.getElementById("spanforh").innerHTML = "Not specified";
            }
            else {
                document.getElementById("spanforh").innerHTML = completedArrayTemp[x].ReleasedYear;

            }
            document.getElementById("spanforext").innerHTML = completedArrayTemp[x].Extent;
            document.getElementById("spanfork").innerHTML = completedArrayTemp[x].Notes;
            document.getElementById("spanforl").innerHTML = completedArrayTemp[x].PointOfContactName;
            document.getElementById("spanform").innerHTML = completedArrayTemp[x].POCEmail;
            document.getElementById("spanforn").innerHTML = completedArrayTemp[x].POCPhoneNumber;
            document.getElementById("spanforo").innerHTML = completedArrayTemp[x].HowToCite;
            document.getElementById("spanforlub").innerHTML = completedArrayTemp[x].LastUpdatedBy;

            document.getElementById("spanforlut").innerHTML = completedArrayTemp[x].LastUpdatedTime;
            document.getElementById("updateCancelButton").onclick = function () { $('.modal_editUpdate').hide(); };

            break;
        }
    }
    $('.modal_editUpdate').show();
    $("#links").hide();
    $('#dtable').show();
    $('#etable').hide();
}

function ViewDataFromDiscards(_uid) {
    for (var x = 0; x < discardedArray.length; x++) {
        if (discardedArray[x].UID == _uid) {
            document.getElementById("spanfora").innerHTML = decodeURIComponent(discardedArray[x].Title);
            document.getElementById("spanfora0").innerHTML = "View Data for " + decodeURIComponent(discardedArray[x].Title);
            document.getElementById("spanforb").innerHTML = discardedArray[x].CategoryName;
            document.getElementById("spanforc").innerHTML = discardedArray[x].MapYear;
            document.getElementById("spanford").innerHTML = discardedArray[x].Organization;
            if (discardedArray[x].NumberOfClasses == 0) {
                document.getElementById("spanfore").innerHTML = "Not specified";
            }
            else {
                document.getElementById("spanfore").innerHTML = discardedArray[x].NumberOfClasses;
            }
            document.getElementById("spanforext").innerHTML = discardedArray[x].Extent;

            document.getElementById("spanforf").innerHTML = discardedArray[x].DataSource;
            document.getElementById("spanforg").innerHTML = discardedArray[x].Status;
            if (discardedArray[x].ReleasedYear == 0) {
                document.getElementById("spanforh").innerHTML = "Not specified";
            }
            else {
                document.getElementById("spanforh").innerHTML = discardedArray[x].ReleasedYear;

            }

            document.getElementById("spanfork").innerHTML = discardedArray[x].Notes;
            document.getElementById("spanforl").innerHTML = discardedArray[x].PointOfContactName;
            document.getElementById("spanform").innerHTML = discardedArray[x].POCEmail;
            document.getElementById("spanforn").innerHTML = discardedArray[x].POCPhoneNumber;
            document.getElementById("spanforo").innerHTML = discardedArray[x].HowToCite;
            document.getElementById("spanforlub").innerHTML = discardedArray[x].LastUpdatedBy;

            document.getElementById("spanforlut").innerHTML = discardedArray[x].LastUpdatedTime;
            document.getElementById("updateCancelButton").onclick = function () { $('.modal_editUpdate').hide(); };

            break;
        }
    }
    $('.modal_editUpdate').show();
    $("#links").hide();
    $('#dtable').show();
    $('#etable').hide();
}
function AddAdmins()
{
    var str = "";
    for (var i = 0; i < emails.length; i++) {
        if (emails[i].Role == "admin") {
            str = str + emails[i].Email;
            str = str + ", ";
        }
    }
    str = str.substring(0, str.length - 2);
    document.getElementById("existingAdmins").innerHTML = str;
    $('.modal_addAdmins').show();
}

function closeP() {
    if (document.getElementById("entered_ids").value.length == 0)
        $('.modal').hide();
    else if (submitted == 0) {
        if (confirm('Close without adding?')) {
            document.getElementById("entered_ids").value = "";
            $('.modal').hide();
        } else {
            // Do nothing!
        }
    }
    else
    $('.modal').hide();
}
function closeVR() {
    $('.modal_editupdate').hide();

}
function closeDR() {
    $('.modal_discardedrequests').hide();
    ViewRequests();
}
function closeR() {

    $('.modal_requests').hide();
    $('.modal_problems').hide();
    $('.modal_reportproblems').hide();
    $('.modal_importData').hide();
    $('.modal_addAdmins').hide();
    $('.modal_about').hide();
    $('.modal_first').hide();
    $('.modal_addMoreData').hide();
    $('.modal_editupdate').hide();
    if (typeof (document.getElementById("jsonTab")) != 'undefined' && document.getElementById("jsonTab") != null) {
        document.getElementById("jsonTab").style.display = "none";
    }
    if (typeof (document.getElementById("jsonTabNew")) != 'undefined' && document.getElementById("jsonTabNew") != null) {
        document.getElementById("jsonTabNew").style.display = "none";
    }

    document.getElementById("selectCountry").selectedIndex = 0;
    document.getElementById("selectUser").selectedIndex = 0;
    document.getElementById("selectStatus").selectedIndex = 0;
    //location.reload();
    //  window.location="http://localhost:61550/Home.aspx";
}
function submitIds() {
    submitted = 1;
    var role = "admin";
    var x = document.getElementById("entered_ids").value;
    if (x.length > 0) {
        var mArr = x.split(',');
        for (var i = 0; i < mArr.length; i++) {
            mArr[i] = mArr[i].trim();
        }
        var mArr_dis = mArr.filter(function (elem, index, self) {
            return index == self.indexOf(elem);
        });
        for (var i = 0; i < mArr_dis.length; i++) {
            if (valid_Id(mArr_dis[i])) {

         
                var found = false;
                var admin = true;
                for (var j = 0; j < emails.length; j++) {
                    if (emails[j].Email == mArr_dis[i]) {

                        found = true;
                        if (emails[j].Role == "user") {
                            admin = false;
                        }
                        else if (emails[j].Role == "admin") {
                            admin = true;
                        }
                        break;
                    }
                }
                if (found) {
                    if (admin) {

                    }
                    else {
                        addUserEmail(role, mArr_dis[i],"");
                        $('.modal').hide();
                        $('.modal_addAdmins').hide();
                        document.getElementById("entered_ids").value = "";
                    }

                }
                else {
                    addUserEmail(role, mArr_dis[i],"");
                    document.getElementById("entered_ids").value = "";
                    $('.modal').hide();
                    $('.modal_addAdmins').hide();
                }
            }
            else {
                alert("Please enter a valid email address");
                document.getElementById("entered_ids").value = "";
                document.getElementById("entered_ids").focus();
            }
        }
    }
    else {
        alert('Please enter atleast one email address');
        document.getElementById("entered_ids").focus();

    }
}
function valid_Id(mail_id) {
    var x = mail_id;
    var atpos = x.indexOf("@");
    var dotpos = x.lastIndexOf(".");
    if (x.length == 0)
        return true;
    else if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
        return false;
    }
    else
        return true;
}
function contains(arr, element) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] === element) {
            return true;
        }
    }
    return false;
}
function ImportData() {
    $('.modal_importData').show();
}
function ViewRequests() {
  
    $("#statusMsg").html("");
    $('#approve').hide();
    $('#discard').hide();
    $('.modal_requests').show();    
   



    document.getElementById("selectUser").selectedIndex = 0;
    var countries = locationCentroids;
    var usrs = newlyAdded;

    var sel = document.getElementById('selectCountry');
    //for (var i = 0; i < countries.Locations.length; i++) {
    //    var opt = document.createElement('option');
    //    opt.innerHTML = countries.Locations[i].Location;
    //    opt.value = countries.Locations[i].Location;
    //    sel.appendChild(opt);
    //}
     
    
    var eids = [];
    eids.push(usrs[0].FullName);
    for (var i = 1; i < usrs.length; i++) {
        if (usrs[i].FullName != eids[i-1])
            eids.push(usrs[i].FullName);
        
        }
    var uids = [];
    for (var i = 0; i < usrs.length; i++) {
        uids.push(usrs[i].UID);
    }
    var unique = eids.filter(function (item, i, ar) { return ar.indexOf(item) === i; });

    var usel = document.getElementById('selectUser');
    for (var i = 0; i < unique.length; i++) {
        var opt = document.createElement('option');
        opt.innerHTML = unique[i];
        opt.value = unique[i];
        
            usel.appendChild(opt);
    }
    if (document.getElementById("selectUser").selectedIndex == 0 && document.getElementById("selectCountry").selectedIndex == 0) {
        CreateTableFromJSONRequests(uids, "Choose a country");
        //highlight_row();
    }

    $('#selectCountry').on('change', function () {

        $("#statusMsg").html("");
        var uid_arr1 = [];
        if ($("select#selectUser option").filter(":selected").text() == "Choose a User") {
            uid_arr1 = uids;
        }
        else {
            for (var i = 0; i < usrs.length; i++) {

                if (usrs[i].FullName == $("select#selectUser option").filter(":selected").text()) {
                    uid_arr1.push(usrs[i].UID);
                }
            }
        }
        CreateTableFromJSONRequests(uid_arr1, this.value);
      //  highlight_row();
        // document.getElementById("jsonTab").style.display = "";
    })
   $('#selectUser').on('change', function () {
       var uid_arr2 = [];
       if ($("select#selectUser option").filter(":selected").text() == "Choose a User") {
           
           uid_arr2 = uids;
       }
       else {
           for (var i = 0; i < usrs.length; i++) {

               if (usrs[i].FullName == $("select#selectUser option").filter(":selected").text()) {
                   uid_arr2.push(usrs[i].UID);
               }
           }
       }

       CreateTableFromJSONRequests(uid_arr2, $("select#selectCountry option").filter(":selected").text());
     //  highlight_row();
   })
   document.getElementById("jsonTab").style.display = "";

}

function ViewDiscardedRequests() {

    $("#statusMsgDiscard").html("");

    $('.modal_discardedrequests').show();
    var usrs = discardedArray;

 
    var uids = [];
    for (var i = 0; i < usrs.length; i++) {
        uids.push(usrs[i].UID);
    }
    if (document.getElementById("selectCountry").selectedIndex == 0) {
        CreateTableFromJSONDiscards(uids, "Choose a country");
    
    }

    $('#selectCountryDiscard').on('change', function () {

        $("#statusMsgDiscard").html("");
        CreateTableFromJSONDiscards(uids, this.value);
    })
    document.getElementById("jsonTabDiscard").style.display = "";

}

function ViewProblems() {
    document.getElementById('selectStatus').selectedIndex = 0;
    $('.modal_problems').show();
    var usrs = reportedProblems;
    var pids = [];
    for (var i = 0; i < usrs.length; i++) {
        pids.push(usrs[i].PID);
    }

    if ( document.getElementById("selectStatus").selectedIndex == 0) {
       CreateTableFromJSON(pids);
    }

    $('#selectStatus').on('change', function () {
        $("#statusMesg").html("");
        var uid_arr1 = [];
        if ($("select#selectStatus option").filter(":selected").text() == "Choose a status") {
            uid_arr1 = pids;
            CreateTableFromJSON(uid_arr1);

        }
        else {
            uid_arr1 = [];
            for (var i = 0; i < usrs.length; i++) {


                if ($("select#selectStatus option").filter(":selected").text() == usrs[i].Status) {
                    uid_arr1.push(usrs[i].PID);
                }

            }
            CreateTableFromJSON(uid_arr1);
        }
        document.getElementById("jsonTabNew").style.display = "";
    })
}
