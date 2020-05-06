defmodule Rsvp.EventsQueries do
    import Ecto.Query

    alias Rsvp.{Repo, Events}

    def get_all do
        Repo.all(from Events)
    end

    def get_all_for_location(location) do
        query =  from e in Events,
            where: e.location == ^location 

        Repo.all(query)
    end

    def get(id) do
        Repo.get(Events, id)
    end

    def create(event) do
        Repo.insert(event)
    end

    def any() do
        Repo.exists?(Events)
    end
end