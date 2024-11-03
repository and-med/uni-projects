# Macro Engine

Macro engine is a text template engine.

## Examples

Example macro definition:
```html
#macro(coloredHeader $color)
    #if($color)
    #else
        #set($color = "#aaaaaa")
    #end
    <div style="background-color:$color;">Hello World!</div>
#end

#macro("#015cac")
```