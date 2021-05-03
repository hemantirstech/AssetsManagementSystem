


// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function() {

	//var IsActive = "@Model.IsActive";
	//alert(IsActive);

	pageSetUp();
		var errorClass = 'invalid';
		var errorElement = 'em';

	var $checkoutForm = $('#MasterLogin-form').validate({
	errorClass		: errorClass,
			errorElement	: errorElement,
			highlight: function(element) {
	$(element).parent().removeClass('state-success').addClass("state-error");
			    $(element).removeClass('valid');
			},
			unhighlight: function(element) {
	$(element).parent().removeClass("state-error").addClass('state-success');
			    $(element).addClass('valid');
			},

		// Rules for form validation
		rules: {

			MasterRegistrationTypeId: {
				required: true
			},
			MasterProfileId: {
				required: true
			},
			UserName: {
				required: true,
				email: true
			},
			Password : {
							required : true
					}
			},

			// Messages for form validation
			messages : {
				MasterRegistrationTypeId : {
					required: 'Please select registration type'
				},
				MasterProfileId : {
					required: 'Please select profile'
				},
				UserName: {
					required: 'Please enter UserName address',
					email: 'Please enter a VALID email address'
				},
				Password: {
					required: 'Please enter password'
				}
			},

			// Do not change code below
			errorPlacement : function(error, element) {
	error.insertAfter(element.parent());
			}
		});

			

	})

