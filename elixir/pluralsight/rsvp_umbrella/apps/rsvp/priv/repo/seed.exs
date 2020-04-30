unless(Rsvp.EventsQueries.any) do 
    Rsvp.EventsQueries.create(Rsvp.Events.changeset(%Rsvp.Events{}, %{eventOn: DateTime.utc_now |> DateTime.add(3600*24*365, :second), title: "Summer Codefest", location: "Omaha, NE"}))
    Rsvp.EventsQueries.create(Rsvp.Events.changeset(%Rsvp.Events{}, %{eventOn: DateTime.utc_now |> DateTime.add(3600*24* 30, :second), title: "NDC London", location: "London"}))
end