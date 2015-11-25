package com.somecompany.horizontalgridlayout;

import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.List;

/**
 * Created by VK on 11/24/2015.
 */
public class TextRecyclerAdapter extends RecyclerView.Adapter<TextRecyclerAdapter.TextViewHolder> {
    public TextRecyclerAdapter(List<String> dataSource) {
        _dataSource = dataSource;
    }

    private List<String> _dataSource;

    @Override
    public TextViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.card_view, parent, false);
        TextViewHolder viewHolder = new TextViewHolder(v);
        return viewHolder;
    }

    @Override
    public void onBindViewHolder(TextViewHolder holder, int position) {
        holder.setText(_dataSource.get(position));
    }

    @Override
    public int getItemCount() {
        return _dataSource.size();
    }

    public static class TextViewHolder extends RecyclerView.ViewHolder {

        public TextViewHolder(View cardView) {
            super(cardView);
            _textView = (TextView)cardView.findViewById(R.id.textView);
        }

        private TextView _textView;

        public void setText(String text) {
            _textView.setText(text);
        }
    }
}
