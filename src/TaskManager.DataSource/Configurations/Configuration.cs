using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace TaskManager.DataSource.Configurations;

public abstract class Configuration
{
    protected static EnumToStringConverter<TEnum> GetStringConverter<TEnum>(int size = 20, bool isUnicode = true)
        where TEnum : struct, Enum
        => new(new ConverterMappingHints(size: size, unicode: isUnicode));
}