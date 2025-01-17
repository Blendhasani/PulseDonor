﻿using FluentValidation;

namespace PulseDonor.MVC.City.Commands
{
	public class AddCityCommand
	{
		public string Name { get; set; }
	}

	public class AddCityCommandValidator : AbstractValidator<AddCityCommand>
	{
        public AddCityCommandValidator()
        {
			RuleFor(x => x.Name).NotEmpty().WithMessage(Resource.RequiredField);
        }
    } 
}
