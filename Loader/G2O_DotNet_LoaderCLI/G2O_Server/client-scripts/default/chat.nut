class ChatLine
{
	constructor(x, y, r, g, b, text)
	{
		__position = { x = x, y = y };
		__drawId = drawCreatePx(x, y, r, g, b, "Font_Old_10_White_Hi.TGA", text);
		
		drawSetVisible(__drawId, true);
	}
	
	function destructor()
	{
		drawDestroy(__drawId);
	}
	
	function update(offset)
	{
		__position.y = offset;
		drawSetPositionPx(__drawId, __position.x, __position.y);
		
		return drawGetHeightPx(__drawId);
	}
	
	__drawId = null;
	__position = null;
};

class ChatLineNickname extends ChatLine
{
	constructor(id, x, y, r, g, b, text)
	{
		local color = getPlayerColor(id);
		
		if (color)
		{
			base.constructor(x, y, r, g, b, text);
			
			__drawNicknameId = drawCreatePx(x, y, color.r, color.g, color.b, "Font_Old_10_White_Hi.TGA", getPlayerName(id) + ": ");
			
			drawSetVisible(__drawNicknameId, true);
			drawSetPositionPx(__drawId, __position.x + drawGetWidthPx(__drawId) - 1, __position.y);
		}
		else
			base.constructor(x, y, 255, 255, 255, text);
	}
	
	function destructor()
	{
		base.destructor();
		drawDestroy(__drawNicknameId);
	}
	
	function update(offset)
	{
		__position.y = offset;
		drawSetPositionPx(__drawNicknameId, __position.x, __position.y);
		drawSetPositionPx(__drawId, __position.x + drawGetWidthPx(__drawNicknameId) - 1, __position.y);
		
		return drawGetHeightPx(__drawNicknameId);
	}
	
	__drawNicknameId = null;
	__position = null;
};

class Chat
{
	constructor(max_lines)
	{
		__lines = [];
		__maxLines = max_lines;
		
		chatInputSetPosition(anx(5), any(5));
	}
	
	function print(r, g, b, text)
	{
		__lines.push(ChatLine(5, 5, r, g, b, text));
		
		if (__lines.len() > __maxLines)
			__lines.remove(0).destructor();
			
		_update();
	}
	
	function clear()
	{
		foreach (line in __lines)
		{
			line.destructor();
		}
		
		__lines.clear();
	}
	
	function setMaxLines(numLines)
	{
		local len = __lines.len();
		if (len > numLines)
		{
			local range = len - numLines;
			for (local i = 0; i < range; ++i)
				__lines.remove(0).destructor();
		}
		
		__maxLines = numLines;
		_update();
	}
	
	
	function _playerMsg(id, r, g, b, text)
	{
		__lines.push(ChatLineNickname(id, 5, 5, r, g, b, text));
		
		if (__lines.len() > __maxLines)
			__lines.remove(0).destructor();
			
		_update();
	}
	
	function _update()
	{
		local offset = 5;
		
		foreach (line in __lines)
		{
			offset += line.update(offset);
		}
		
		chatInputSetPosition(anx(5), any(offset));
	}
	
	__lines = null;
	__maxLines = 0;
}

GlobalChat <- Chat(15);


function msgHandler(id, r, g, b, msg)
{
	if (id == -1)
		GlobalChat.print(r, g, b, msg);
	else
		GlobalChat._playerMsg(id, r, g, b, msg);
}

addEventHandler("onPlayerMessage", msgHandler);


function cmdHandler(cmd, params)
{
	switch (cmd)
	{
	case "chatclear":
		GlobalChat.clear();
		break;
		
	case "chatlines":
		local arg = sscanf("d", params);
		
		if (arg)
		{
			local lines = arg[0];
			if (lines < 3)
				GlobalChat.print(255, 0, 0, "Minimum lines count is 3!");
			else if (lines > 30)
				GlobalChat.print(255, 0, 0, "Maximum lines count is 30!");
			else
			{
				GlobalChat.print(0, 255, 0, "Chat lines count changed to: " + lines);
				GlobalChat.setMaxLines(lines);
			}
		}
		else
			GlobalChat.print(255, 0, 0, "Type: /chatlines num_lines");
			
		break;
	}
}

addEventHandler("onCommand", cmdHandler);

function keyHandler(key)
{
	if (chatInputIsOpen())
	{
		switch (key)
		{
		case KEY_RETURN:
			chatInputSend();
			break;
			
		case KEY_ESCAPE:
			chatInputClose();
			break;
		}
	}
	else if (key == KEY_T)
	{
		chatInputOpen();
	}
}

addEventHandler("onKey", keyHandler);

// Loaded
print("chat.nut loaded...")