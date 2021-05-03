

// DO NOT REMOVE : GLOBAL FUNCTIONS!

	$(document).ready(function() {

		

		pageSetUp();
			var errorClass = 'invalid';
			var errorElement = 'em';

		var $checkoutForm = $('#MasterEmployee-form').validate({
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
				MasterSalutationId: {
					required: true
                },
				EmployeeName : {
								required : true
				},
				MasterCompanyId : {
								required : true
				},
				MasterBranchId: {
							required: true
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
					MasterSalutationId : {
						required : 'Please select salutation (Mr. or Mrs.)'
					},
					EmployeeName : {
						required: 'Please enter employee name'
					},
					MasterCompanyId: {
						required: 'Please select company'
					},
					MasterBranchId: {
						required: 'Please select branch'
					},
					Email : {
						required : 'Please enter email address',
						email : 'Please enter a VALID email address'
					},
					MobileNumber : {
						required : 'Please enter mobile number'
					}
				},

				// Do not change code below
				errorPlacement : function(error, element) {
		error.insertAfter(element.parent());
				}
			});

			

		



		})

