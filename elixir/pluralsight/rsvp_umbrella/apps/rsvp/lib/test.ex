defmodule Math1 do
    def update_display_cache(context, text, line_no, position,   _), do: 1
  	def update_display_cache(context, display_line, line_no, position, _), do: 2
end

alias Math1
Math1.update_display_cache(1,1,1,1,1)
