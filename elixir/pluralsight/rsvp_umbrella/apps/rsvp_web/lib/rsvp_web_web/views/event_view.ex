defmodule RsvpWebWeb.EventView do
    use RsvpWebWeb, :view

    def format_date(date) do
        date
        |> DateTime.to_date
        |> Date.to_string
    end

    def format_time(date) do
        date 
        |> DateTime.to_time()
        |> Time.to_string()
    end
end