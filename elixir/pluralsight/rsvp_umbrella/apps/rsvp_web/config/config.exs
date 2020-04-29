# This file is responsible for configuring your application
# and its dependencies with the aid of the Mix.Config module.
#
# This configuration file is loaded before any dependency and
# is restricted to this project.

# General application configuration
use Mix.Config

# Configures the endpoint
config :rsvp_web, RsvpWebWeb.Endpoint,
  url: [host: "localhost", port: 4004],
  secret_key_base: "t8INzjOwAwm+3tg3xcVTtXhNXZs9Ok+5EH/CkUadT3T7BZmwzQyfSbA0BgthDvbk",
  render_errors: [view: RsvpWebWeb.ErrorView, accepts: ~w(html json), layout: false],
  pubsub_server: RsvpWeb.PubSub,
  live_view: [signing_salt: "WHgbELuA"]

# Configures Elixir's Logger
config :logger, :console,
  format: "$time $metadata[$level] $message\n",
  metadata: [:request_id]

# Use Jason for JSON parsing in Phoenix
config :phoenix, :json_library, Jason

# Import environment specific config. This must remain at the bottom
# of this file so it overrides the configuration defined above.
import_config "#{Mix.env()}.exs"
