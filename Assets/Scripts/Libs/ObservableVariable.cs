using System;
using System.Collections.Generic;

public class ObservableVariable<T>
{
    private T value;

    public T Value
    {
        get { return value; }
        set
        {
            if (!EqualityComparer<T>.Default.Equals(this.value, value))
            {
                this.value = value;
                OnValueChanged?.Invoke(this.value);
            }
        }
    }

    public event Action<T> OnValueChanged;

    public ObservableVariable(T initialValue)
    {
        value = initialValue;
    }

    // Static instance property
    public static ObservableVariable<T> Instance { get; private set; }

    // Static constructor to initialize the instance
    static ObservableVariable()
    {
        // Initialize the static instance with a default value if needed
        Instance = new ObservableVariable<T>(default(T));
    }
}
