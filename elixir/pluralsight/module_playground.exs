defmodule ModulePlayground do
    import IO, only: [puts: 1]
    import Kernel, except: [inspect: 1]

    # alias ModulePlayground.Misc.Util.Math
    alias ModulePlayground.Misc.Util.Math, as: MyMath

    require Integer
 
    def say_here do
        inspect "I'm here 2"
    end

    def inspect(param1) do
        puts "Starting Output"
        puts param1
        puts "Ending Output"
    end

    def print_sum do
        # puts "priting sum"
        # Math.add(1,4)
        MyMath.add(1,4)
    end

    def print_is_even(num) do
        puts "Is #{num} even? #{Integer.is_even(num)}"
    end

    def print_first(list) do
        hd(list)
    end

    def f(list, val \\ nil)
    def f([head | _], _) do
        head
    end
    def f([], val), do: val 

 
    # def f([head | _], _) do
    #     head
    # end
    # def f([], val \\ nil), do: val 

    # def f([]), do: nil
    # def f(list) when length(list) == 0, do: nil
        
    # def f([head | _]) do
    #     head
    # end

    def add(list, val \\ 0) do
            private()
        [val | list]
    end

    defp private() do
        puts "private call"
    end
end