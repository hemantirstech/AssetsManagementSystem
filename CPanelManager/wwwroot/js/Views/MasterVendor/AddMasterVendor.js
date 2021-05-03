

// DO NOT REMOVE : GLOBAL FUNCTIONS!

	$(document).ready(function() {

		

		pageSetUp();
			var errorClass = 'invalid';
			var errorElement = 'em';

		var $checkoutForm = $('#MasterVendor-form').validate({
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

						VendorTitle : {
								required : true
						},
						ContactPerson : {
								required : true
						},
						Email : {
							required : true,
							email : true
						},
						MobileNumber : {
							required : true
						}
				},

				// Messages for form validation
				messages : {
					VendorTitle : {
						required : 'Please enter Vendor title'
					},
					ContactPerson : {
						required: 'Please enter contact person name'
					},					
					Email : {
						required : 'Please enter your email address',
						email : 'Please enter a VALID email address'
					},
					MobileNumber : {
						required : 'Please enter your mobile number'
					}
				},

				// Do not change code below
				errorPlacement : function(error, element) {
		error.insertAfter(element.parent());
				}
			});

			

		



		})

