using Domain.Passengers.DataAccess;
using Domain.Passengers.Models;
using Domain.Passengers.Services;
using Domain.Services.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Pessengers;

public class PassengerNoSpecificationService(IPassengerNoSpecificationProvider passengerNoSpecificationProvider) : IPassengerNoSpecificationService
{
    private readonly IPassengerNoSpecificationProvider _passengerNoSpecificationProvider = 
        passengerNoSpecificationProvider ?? throw new ArgumentException(nameof(_passengerNoSpecificationProvider));

    /// <inheritdoc />
    public Task<IReadOnlyCollection<PassengerFlightNumModel>> GetPessengersFlightNoAsync(GetPassengerRequest request) =>
         _passengerNoSpecificationProvider.GetPessengersFlightNoAsync(request);

    /// <inheritdoc />
    public Task<IReadOnlyCollection<PassengerPhoneModel>> GetPessengersPhoneAsync(GetPassengerRequest request) =>
        _passengerNoSpecificationProvider.GetPessengersPhoneAsync(request);

    /// <inheritdoc />
    public Task<IReadOnlyCollection<PassengerSeatModel>> GetPessengersSeatsAsync(GetPassengerRequest request) =>
        _passengerNoSpecificationProvider.GetPessengersSeatsAsync(request);
}

