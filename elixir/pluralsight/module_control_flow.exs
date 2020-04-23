defmodule ControlFlow do
    
    defp day_abbr(day) do
        cond do
            day == :Monday -> "M"
            day == :Tuesday -> "Tu"
            day == :Wednesday -> "W"
            true -> "Invalid day"
        end
    end

    defp describe_day(date) do
        case date do
            {1, _, _} -> "1st day of month"
            {25, 12, _} -> "Merry Christmas"
            {_, m, _} when m > 12 -> "Invalid month"
            {_, _, _} -> "Average day"                
        end
    end

    defp rec([], _), do: []
    defp rec([head | tail], f) do
        [f.(head) | rec(tail, f)]
    end

    def play() do
        list = [1,2,3]
        if (length(list) != 0) do
            IO.puts "nonzero in if"
        else 
            IO.puts "zero in if"
        end

        IO.puts ""
        IO.puts day_abbr(:Monday)
        IO.puts day_abbr(:Mon)

        IO.puts ""
        IO.puts describe_day({01, 12, 2020})
        IO.puts describe_day({25, 12, 2020})
        IO.puts describe_day({25, 17, 2020})

        IO.puts ""
        IO.inspect rec([1,2,3], &(&1 + 1))


        :ok
    end

    
end