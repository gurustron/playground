defmodule RsvpWebWeb.EventViewTests do
    use RsvpWebWeb.ConnCase, async: true

    @tag current: true
    test "should conver date to ..." do
        dt1 = %DateTime{year: 2000, month: 2, day: 29, zone_abbr: "AMT",
                hour: 23, minute: 0, second: 7, microsecond: {0, 0},
                utc_offset: -14400, std_offset: 0, time_zone: "America/Manaus"}
        formatted = RsvpWebWeb.EventView.format_date(dt1)
        assert formatted = formatted |> DateTime.to_date() |> Date.to_string
    end
end