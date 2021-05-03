

// DO NOT REMOVE : GLOBAL FUNCTIONS!

	$(document).ready(function() {

		

		pageSetUp();
			var errorClass = 'invalid';
			var errorElement = 'em';

		var $checkoutForm = $('#MasterBranch-form').validate({
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

						BranchTitle : {
								required : true
						},
						ContactPerson : {
								required : true
						},
						Address1: {
							required: true
						},
						MasterCountryId: {
							required: true
						},
						MasterStateId: {
							required: true
						},
						City: {
							required: true
						},
						PinCode: {
							required: true,
							digits: true
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
					BranchTitle : {
						required : 'Please enter branch title'
					},
					ContactPerson : {
						required: 'Please enter contact person name'
					},
					Address1: {
						required: 'Please enter address'
					},
					MasterCountryId: {
						required: 'Please select country'
					},
					MasterStateId: {
						required: 'Please select state'
					},
					City: {
						required: 'Please enter your city'
					},
					PinCode: {
						required: 'Please enter pincode',
						digits: 'Digits only please'
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

