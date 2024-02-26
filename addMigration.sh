dotnet ef migrations add AddedCustomerPolimorphism -p MovieStore.Infrastructure -s MovieStoreApi

dotnet ef migrations remove ConvertedMoneyValueToDecimal -p MovieStore.Infrastructure -s MovieStoreApi

dotnet ef database update -TargetMigration:MoneySpentTypeChangeToDouble -p MovieStore.Infrastructure -s MovieStoreApi