$(document).ready(function () {
    $('#btnAddGroup').click(function () {
        $("#add_group").slideToggle("slow");
    });

    $('#add_group form').submit(function () {
        
        if ($(this).valid()) {
            
            var gender = $("#gender").val();
            var belt = $("#belt").val();
            var weight = $("#weight").val();

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
                    currentText += "<div><img src=\"/images/Delete-icon.png\" /> " + result + "</div>";
                    $("#category").html (currentText);
                }
            });
        }
        return false;
    });
    
});