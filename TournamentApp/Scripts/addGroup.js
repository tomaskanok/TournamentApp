$(document).ready(function () {
    $('#btnAddGroup').click(function () {
        $("#add_group").slideToggle("slow");
    });

    $('#add_group form').submit(function () {
        
        if ($(this).valid()) {
            
            var gender = $("#ddlGender option:selected").val();
            var belt = $("#ddlBelt option:selected").val();
            var weight = $("#weight").val();

            alert(gender + belt + weight);

            $.ajax({
                url: '/Group/AddGroup',
                type: "POST",
                data: {
                    weight: weight,
                    belt: belt,
                    gender: gender
                },
                success: function (result) {
                    var currentText = $("#category").html();
                    currentText += "<div><img class=\"deleteGroup hovercursor\" src=\"/images/Delete-icon.png\" /> " + result + "</div>";
                    $("#category").html (currentText);
                }
            });
        }
        return false;
    });
    
});