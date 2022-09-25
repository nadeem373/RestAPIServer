using FluentValidation;
using FluentValidation.Attributes;
using Newtonsoft.Json;

namespace RestAPIServer.Models
{
    [Validator(typeof(CustomerValidator))]
    public class Customer
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }

    /// <summary>
    /// CustomerValidator
    /// </summary>
    public class CustomerValidator : AbstractValidator<Customer>
    {
        /// <summary>  
        /// Validator rules for Customer  
        /// </summary>  
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("The First Name cannot be blank.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("The Last Name cannot be blank..");

            RuleFor(x => x.Age).GreaterThan(18).WithMessage("The Customer Agee must be at greather than 18.");
        }
    }

}