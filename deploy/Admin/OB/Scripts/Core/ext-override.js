//*** This function is overriden due to eliminate unexpected row 
//*** selection exception in group detail grid
Ext.override(Ext.grid.GridView,
{
    getResolvedXY: function(resolved) 
    {
        try
        {
            if (!resolved) {
                return null;
            }
            var s = this.scroller.dom, c = resolved.cell, r = resolved.row;
            return c ? Ext.fly(c).getXY() : [this.el.getX(), Ext.fly(r).getY()];
        }catch(e)
        {
        }
    }
 });

//*** This function is overriden due to eliminate unexpected row 
//*** selection exception in group detail grid 
Ext.override(Ext.grid.GridView,
{
    getCell: function(row, col) 
    {
        try
        {
            return this.getRow(row).getElementsByTagName('td')[col];
        }catch(e)
        {
        }
    }
 });
 

Ext.override(Ext.dd.DragSource, 
{
    handleMouseDown: function(e) 
    {        
        if(this.dragging) 
        {
                return;
        }
        var data = this.getDragData(e);        
        var isDraggableData = OBSettings.IsDraggable(data.header.childNodes[0].children[0].nextSibling.data);
        if(data && this.onBeforeDrag(data, e) !== false && isDraggableData )
        {
            this.dragData = data;
            this.proxy.stop();
            Ext.dd.DragSource.superclass.handleMouseDown.apply(this, arguments);
        } 
    }
});

Ext.override(Ext.grid.GridView, {
    beforeColMenuShow: function() {
        var cm = this.cm, colCount = cm.getColumnCount();
        this.colMenu.removeAll();        
        for (var i = 0; i < colCount; i++) {
            if (cm.config[i].fixed !== true && cm.config[i].hideable !== false && OBSettings.IsShowableInHeaderForContextMenu(cm.getColumnHeader(i))) {
                this.colMenu.add(new Ext.menu.CheckItem({
                    itemId: "col-" + cm.getColumnId(i),
                    text: cm.getColumnHeader(i),
                    checked: !cm.isHidden(i),
                    hideOnClick: false,
                    disabled: cm.config[i].hideable === false
                }));
            }
        }
    }
});

Ext.override(Ext.grid.GridView, 
{
    updateAllColumnWidths: function() {
        try
        {
            var tw = this.getTotalWidth(),
                clen = this.cm.getColumnCount(),
                ws = [],
                len,
                i;
            for (i = 0; i < clen; i++) {
                ws[i] = this.getColumnWidth(i);
            }
            this.innerHd.firstChild.style.width = this.getOffsetWidth();
            this.innerHd.firstChild.firstChild.style.width = tw;
            this.mainBody.dom.style.width = tw;
            for (i = 0; i < clen; i++) {
                var hd = this.getHeaderCell(i);
                hd.style.width = ws[i];
            }

            var ns = this.getRows(), row, trow;
            for (i = 0, len = ns.length; i < len; i++) {
                row = ns[i];
                row.style.width = tw;
                if (row.firstChild) {
                    row.firstChild.style.width = tw;
                    trow = row.firstChild.rows[0];
                    for (var j = 0; j < clen; j++) {
                        trow.childNodes[j].style.width = ws[j];
                    }
                }
            }

            this.onAllColumnWidthsUpdated(ws, tw);
        }catch(e)
        {
        }
    }
});

Ext.override(Ext.grid.GridView, {
    updateColumnWidth: function(col, w, tw) {      // not related, but        
        var grid = this.innerHd.firstChild.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
        if (OBSettings.EXPANDED_GROUP_ID > -1 && !(grid.id.startsWith('detailDiv'))) {
            alert("Please close detail grid to resize gropped grid");
            return;
        }
        var w = this.getColumnWidth(col);
        var tw = this.getTotalWidth();
        this.innerHd.firstChild.style.width = this.getOffsetWidth();
        this.innerHd.firstChild.firstChild.style.width = tw;
        this.mainBody.dom.style.width = tw;
        var hd = this.getHeaderCell(col);
        hd.style.width = w;

        var ns = this.getRows(), row;
        for (var i = 0, len = ns.length; i < len; i++) {
            row = ns[i];
            row.style.width = tw;
            if (row.firstChild) {
                row.firstChild.style.width = tw;
                row.firstChild.rows[0].childNodes[col].style.width = w;
            }
        }

        this.onColumnWidthUpdated(col, w, tw);
    }
});


Ext.override(Ext.data.Store, {
    sort: function CustomSorting(fieldName, dir, cellId) 
    {
          //For custom group field restriction
//        if (!OBSettings.IsCustomSortable(fieldName)) 
//        {
//            return;
//        }
    
        var detailGrid;
        try 
        {            
            detailGrid = Ext.get(cellId).up('DIV.detailGrid');
        } catch (e) 
        {
            return;
        }
               
        var sortableGrid = detailGrid ? "DETAIL_GRID" : "MAIN_GRID";                
        OBSettings.ACTIVE_GRID = sortableGrid;
        if (sortableGrid == 'DETAIL_GRID') {
            if (dir) {
                OBSettings.SQL_DETAIL_ORDER_BY = fieldName;
                OBSettings.SQL_DETAIL_ORDER_DIR = dir;
            } else if (OBSettings.SQL_DETAIL_ORDER_BY != fieldName) {
                OBSettings.SQL_DETAIL_ORDER_BY = fieldName;
                OBSettings.SQL_DETAIL_ORDER_DIR = "ASC";
            } else {
                OBSettings.SQL_DETAIL_ORDER_BY = fieldName;
                OBSettings.SQL_DETAIL_ORDER_DIR = (OBSettings.SQL_DETAIL_ORDER_DIR == "ASC") ? "DESC" : "ASC";
            }
           
            OBSettings.ShowDetailLoadingImage();
            setTimeout('OBSettings.CreateNormalGrid()', 1);
        } else {
            if (dir) {
                OBSettings.SQL_ORDER_BY = fieldName;
                OBSettings.SQL_ORDER_DIR = dir;
            } else if (OBSettings.SQL_ORDER_BY != fieldName) {
                OBSettings.SQL_ORDER_BY = fieldName;
                OBSettings.SQL_ORDER_DIR = "ASC";
            } else {
                OBSettings.SQL_ORDER_BY = fieldName;
                OBSettings.SQL_ORDER_DIR = (OBSettings.SQL_ORDER_DIR == "ASC") ? "DESC" : "ASC";
            }
           
            OBSettings.EXPANDED_GROUP_ID = -1;
            OBSettings.ShowMainLoadingImage();
            
            if (OBSettings.SQL_GROUP_BY != "NONE") 
            {                
                setTimeout('OBSettings.CreateGroupByGrid()', 1);
            } else 
            {                
                setTimeout('OBSettings.CreateNormalGrid()', 1);
            }
        }
    }
});


Ext.override(Ext.grid.RowSelectionModel, {
    selectRow: function CustomSelectRow(index, keepExisting, preventViewNotify) {
        if (this.isLocked() || (index < 0 || index >= this.grid.store.getCount()) || (keepExisting && this.isSelected(index))) {
            return;
        }
        if (OBSettings.ACTIVE_GRID == 'DETAIL_GRID' && this.grid.id == "GroupGrid") {
            //this.clearSelections();
            return;
        }
      
        var r = this.grid.store.getAt(index);
        if (r && this.fireEvent("beforerowselect", this, index, keepExisting, r) !== false) {
            if (!keepExisting || this.singleSelect) {
                this.clearSelections();
            }
            this.selections.add(r);
            this.last = this.lastActive = index;
            if (!preventViewNotify) {
                this.grid.getView().onRowSelect(index);
            }
            this.fireEvent("rowselect", this, index, r);
            this.fireEvent("selectionchange", this);
        }

        OBSettings.SetActiveRowNav();
        if(this.grid.id=='NormalGrid')
        {
            OBSettings.SetDetailKeys(r);
        }
    }

});
