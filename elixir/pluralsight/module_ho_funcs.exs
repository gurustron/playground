defmodule FuncP do
    # import IO, only: [puts: 1]
    alias ModulePlayground.Misc.Util.Math, as: MyMath

    def play() do
        list = [1,2,3]
        
        IO.puts "capture func"
        IO.inspect Enum.map(list, &MyMath.square/1)
        IO.inspect Enum.reduce(list, 0, &MyMath.add/2)
        
        IO.puts "anon func"
        IO.inspect Enum.map(list, fn(x) -> x*x end)
        IO.inspect Enum.reduce(list, 0, fn(a, b) -> a+b end)
        
        IO.puts "short anon"
        IO.inspect Enum.map(list, &(&1 * &1))
        IO.inspect Enum.reduce(list, 0, &(&1 + &2))

        IO.puts "accept func arg"
        apply_f(&(IO.inspect &1), [1])

        :ok
    end

    def apply_f(f, a) do
        f.(a)
    end
end