using System;

public class ScoreModel
{
    private int _score = 0;

    public int Score => _score;

    public void Increase(int value)
    {
        if (value < 0)
            throw new ArgumentException("Value cannot be less than 0", nameof(value));

        _score += value;
    }
}
