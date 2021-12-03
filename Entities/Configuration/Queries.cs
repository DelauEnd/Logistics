using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class Queries
    {
        public readonly static string OnCargoUpdate = 
            "create trigger OnCargoUpdate " +
            "on Cargoes for update " +
            "as " +
            "begin " +
            "declare @orderId uniqueidentifier " +
            "declare @orderStatus int " +
            "set @orderId = ( select OrderId from inserted ) " +
            "set @orderStatus = ( select Status from Orders where OrderId = @orderId ) " +
            "if @orderStatus = 2 " +
            "begin " +
            "rollback transaction " +
            "end " +
            "declare @arrivalDate datetime2 " +
            "declare @departureDate datetime2 " +
            "declare @routeId uniqueidentifier " +
            "set @arrivalDate = ( select ArrivalDate from inserted ) " +
            "set @departureDate = ( select DepartureDate from inserted ) " +
            "set @routeId = ( select RouteId from inserted ) " +
            "update Cargoes " +
            "set DepartureDate = @departureDate, " +
            "ArrivalDate = @arrivalDate " +
            "where RouteId = @routeId and OrderId = @orderId " +
            "end";

        public readonly static string OnCargoCreate =
            "create trigger OnCargoCreate " +
            "on Cargoes for insert " +
            "as " +
            "begin " +
            "declare @orderId uniqueidentifier " +
            "declare @orderStatus int " +
            "set @orderId = ( select OrderId from inserted ) " +
            "set @orderStatus = ( select Status from Orders where OrderId = @orderId ) " +
            "if @orderStatus = 2 " +
            "begin " +
            "rollback transaction " +
            "end " +
            "end";

        public readonly static string SetCompletedOrderStatusIfAllCargoesAreCompleted =
            "create trigger SetCompletedOrderStatusIfAllCargoesAreCompleted " +
            "on Cargoes after update " +
            "as " +
            "begin " +
            "declare @orderId uniqueidentifier " +
            "set @orderId = ( select OrderId from inserted ) " +
            "update Orders set Status = 2 " +
            "where OrderId = @orderId " +
            "and 2 = all ( select Status from Cargoes where OrderId = @orderId ) " +
            "end";
    }
}
