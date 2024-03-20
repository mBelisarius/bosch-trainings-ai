namespace Artin.Collections;

public class MultiMap<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, List<TValue>> _map;

    public int Count => _map.Count;

    public IReadOnlyCollection<TValue> Values
    {
        get
        {
            var list = new List<TValue>(Count);
            foreach (var pair in _map)
                list.AddRange(pair.Value);

            return list;
        }
    }

    public MultiMap()
    {
        _map = new Dictionary<TKey, List<TValue>>();
    }

    public void Add(TKey key, TValue value)
    {
        if (!_map.TryGetValue(key, out var values))
        {
            values = new List<TValue>();
            _map[key] = values;
        }

        values.Add(value);
    }

    public IReadOnlyList<TValue> this[TKey key] 
        => _map.TryGetValue(key, out var values) ? values : new List<TValue>();
}