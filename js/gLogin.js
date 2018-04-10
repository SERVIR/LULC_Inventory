

function onSignIn(googleUser) {

    loggedIn = 1;

    $('#gSignIn').hide();

    var profile = googleUser.getBasicProfile();

    //console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
    //console.log('Name: ' + profile.getName());
    //console.log('Image URL: ' + profile.getImageUrl());
    //console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.
    //console.log('token: ' + googleUser.getAuthResponse().id_token);
    $('#addmorebutton_link').show();

    $('#uname').html('Welcome ' + profile.getName());
    mail_id = profile.getEmail();
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
            $('#ViewRequestsResp').show();

            $('#editlink').show();
            $('#deletelink').show();

            $('#unapprovelink').show();
            
            adminLoggedIn = 1;
        }
        else {
                 $('#AddAdmins').hide();
                 $('#AddAdminsResp').hide();

                 $('#ViewRequests').hide();
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
        Shadowbox.open({
            content: '<div style="color:black;font-size:20px">Click on a country and the <b>"+"</b> button on the right side will let you add more data!' +
                     '</div>',
            player: "html",
            title: "Did you know?",
            height: "150px"
        });
    });

}
function signOut() {
    adminLoggedIn = 0;
    var auth2 = gapi.auth2.getAuthInstance();
    
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
    window.location.href = "http://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://servirglobal.net/MapResources/LULC_Africa/Home.aspx";
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


    Shadowbox.open({
        content: '<div style="color:black;">       ' +
                        '  <p  style="margin-left: 0.7vw" >Current Admins are:</p>' +

            '  <p id="existingAdmins" style="margin-left: 0.7vw;margin-right: 0.7vw" >' + str + '</p>' +
          '  <p style="margin-left: 0.7vw">Enter the email ids of admins (separate multiple entries by commas)</p>' +
           ' <textarea style="margin-left: 0.7vw" id="entered_ids" rows="4" cols="50" placeholder="xyz@gmail.com,abc@gmail.com"></textarea>'+
           ' <button style="margin-left: 0.7vw; margin-top: 0.5vw;padding:8px 16px 8px;float:right;" id="submit_ids" onclick="submitIds()">Add Admin(s)</button>' +
                 '</div>',
        player: "html",
        title: "Application Administrators",
        height: "350px"
    });
  
    //$('.modal').show();
   // document.getElementById("entered_ids").focus();

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
function closeR() {

    $('.modal_requests').hide();
    document.getElementById("jsonTab").style.display = "none";
    document.getElementById("selectCountry").selectedIndex = 0;
    document.getElementById("selectUser").selectedIndex = 0;
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
                        document.getElementById("entered_ids").value = "";
                    }

                }
                else {
                    addUserEmail(role, mArr_dis[i],"");
                    document.getElementById("entered_ids").value = "";
                    $('.modal').hide();
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
function ViewRequests() {
  

    $('#approve').hide();
    $('#discard').hide();
    $('.modal_requests').show();    
    document.getElementById('selectCountry').selectedIndex = 0;
    document.getElementById("selectUser").selectedIndex = 0;
    var countries = locationCentroids;
    var usrs = newlyAdded;


    var sel = document.getElementById('selectCountry');
    for (var i = 0; i < countries.Locations.length; i++) {
        var opt = document.createElement('option');
        opt.innerHTML = countries.Locations[i].Location;
        opt.value = countries.Locations[i].Location;
        sel.appendChild(opt);
    }
    var eids = [];
    for (var i = 0; i < usrs.length; i++) {
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
        CreateTableFromJSON(uids, "Choose a country");
        
        highlight_row();
    }

    $('#selectCountry').on('change', function () {
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
        CreateTableFromJSON(uid_arr1, this.value);
        highlight_row();
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

       CreateTableFromJSON(uid_arr2, $("select#selectCountry option").filter(":selected").text());
       highlight_row();
        //document.getElementById("jsonTab").style.display = "";
   })
   document.getElementById("jsonTab").style.display = "";

}
